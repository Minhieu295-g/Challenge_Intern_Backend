using Application.Settings;
using Common.Controllers;
using DotNetTraining.Domains.Dtos;
using DotNetTraining.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetTraining.Controllers.v1
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : BaseV1Controller<AuthService, ApplicationSetting>
    {
        private readonly AuthService _authService;
        public AuthController(IServiceProvider services, IHttpContextAccessor httpContextAccessor) : base(services, httpContextAccessor)
        {
            this._authService = services.GetRequiredService<AuthService>();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            var token = await this._authService.Login(request.email, request.password);
            return Success(token);
        }
        
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var result = await _authService.RefreshAccessToken(request.RefreshToken);
            return Ok(result);
        }

    }
}
