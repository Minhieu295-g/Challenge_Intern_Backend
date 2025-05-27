using Common.Application.Exceptions;
using Common.Application.Models;
using Common.Application.Settings;
using Common.Security;
using Common.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Settings;
using static Common.Utilities.JwtUtil;
using System.Data;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using ApplicationException = Common.Application.Exceptions.ApplicationException;

namespace Common.Application.Middlewares
{
    public class AuthenticateMiddleware : BaseMiddleware
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthenticateMiddleware> _logger;
        private readonly PermissionSetting _permissionSetting;
        public AuthenticateMiddleware(RequestDelegate next, IConfiguration configuration, ILogger<AuthenticateMiddleware> logger, PermissionSetting permissionSetting) : base(next)
        {
            _configuration = configuration;
            _logger = logger;
            _permissionSetting = permissionSetting;
        }

        public override async Task Invoke(HttpContext context, IServiceProvider services, IConfiguration configuration)
        {
            try
            {

                var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
                if (authHeader != null && authHeader.StartsWith("Bearer "))
                {
                    var token = authHeader.Substring("Bearer ".Length).Trim();


                    var jwtTokenSetting = _configuration.GetSection("JwtTokenSetting").Get<JwtTokenSetting>();
                    var userModel = ValidateToken(token, jwtTokenSetting!);

                    foreach (var permission in _permissionSetting.ApiPermissions)
                    {
                        var endpoint = permission.Key;
                        var roles = string.Join(", ", permission.Value);
                        _logger.LogInformation($"[PERMISSION] {endpoint} => Roles: {roles}");
                    }


                    if (userModel != null)
                    {
                        var endpoint = context.GetEndpoint();
                        var routeEndpoint = endpoint as RouteEndpoint;

                        var routePattern = routeEndpoint?.RoutePattern?.RawText ?? "";
                        routePattern = routePattern.StartsWith("/") ? routePattern : "/" + routePattern;
                        var requestedEndpoint = $"{context.Request.Method} - {routePattern}";


                        _logger.LogInformation("Request Endpoint: {Endpoint}", requestedEndpoint);

                        context.Items["User"] = userModel;
                        var normalizedRole = userModel.Role.ToLowerInvariant();

                        if (_permissionSetting.ApiPermissions.TryGetValue(requestedEndpoint, out var allowedRoles))
                        {
                            var normalizedAllowedRoles = allowedRoles.Select(r => r.Trim().ToLowerInvariant()).ToHashSet();

                            var userRoles = normalizedRole
                                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                                .Select(r => r.Trim().ToLowerInvariant());

                            if (!userRoles.Any(role => normalizedAllowedRoles.Contains(role)))
                            {
                                _logger.LogWarning($"Access denied for role(s) '{normalizedRole}' to endpoint {requestedEndpoint}.");
                                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                                await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                                {
                                    message = "Access denied: You do not have permission to access this API."
                                }));
                                return;
                            }
                        }
                        else
                        {
                            _logger.LogWarning($"Endpoint {requestedEndpoint} not found in permission settings.");
                            context.Response.StatusCode = StatusCodes.Status403Forbidden;
                            await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                            {
                                message = "Access denied: Endpoint not configured in permission settings."
                            }));
                            return;
                        }

                    }

                    else
                    {
                        _logger.LogWarning("Token validation failed.");
                    }
                }
                else
                {
                    _logger.LogWarning("Authorization header is missing or invalid.");
                }

                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Authentication middleware encountered an error: {ex.Message}");
                var appException = GetApplicationException(ex);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)appException.HttpStatusCode;

                var response = appException.GetErrorResponse();
                var json = JsonConvert.SerializeObject(response, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });

                await context.Response.WriteAsync(json);
            }
        }
        private AuthenticatedUserModel? ValidateToken(string token, JwtTokenSetting jwtTokenSetting)
        {
            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenSetting.SymmetricSecurityKey)),
                    ValidateIssuer = true,
                    ValidIssuer = jwtTokenSetting.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtTokenSetting.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out _);
                var identity = (ClaimsIdentity)principal.Identity!;

                if (identity != null)
                {
                    var roleClaim = identity.FindFirst(ClaimTypes.Role);
                    if (roleClaim != null)
                    {
                        var role = roleClaim.Value;
                    }
                }
                return UserClaims.GetAuthenticatedUser(principal.Claims);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Token validation failed: {ex.Message}");
                return null;
            }
        }

        private static ApplicationException GetApplicationException(Exception ex)
        {
            return ex is ApplicationException applicationException
                ? applicationException
                : new InternalErrorException(ex.Message);
        }
    }
}
