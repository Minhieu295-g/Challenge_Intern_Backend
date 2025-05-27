using Application.Settings;
using Azure.Core;
using Common.Application.CustomAttributes;
using Common.Application.Models;
using Common.Application.Settings;
using Common.Services;
using Common.Utilities;
using DotNetTraining.Domains.Dtos;
using DotNetTraining.Domains.Entities;
using DotNetTraining.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
namespace DotNetTraining.Services
{
    [ScopedService]
    public class AuthService(IServiceProvider services, ApplicationSetting setting, IDbConnection connection) : BaseService(services)
    {
        private readonly UserRepository _repo = new(connection);
        private readonly TokenRepository _tokenRepo = new(connection);
        private readonly JwtTokenSetting _jwtTokenSetting = setting.JwtTokenSetting;

        public async Task<TokenResponseDto> Login(string email, string password)
        {
            var user = await _repo.GetUserByEmail(email);
            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, user.Password, password);
            if (result != PasswordVerificationResult.Success)
            {
                throw new Exception("username or password isn't correct");
            }
            var role = await _repo.GetRoleUserByEmail(user.Email);
            var authenticatedUser = new AuthenticatedUserModel
            {
                UserId = user.Id,
                UserName = user.Email,
                FirstName = user.Name,
                LastName = user.Name,
                Role = role ?? AuthenticatedUserModel.GuestRole
            };
            var (accessToken, refreshToken) = JwtUtil.CreateJwtAndRefreshToken(_jwtTokenSetting, authenticatedUser, role);
            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtHandler.ReadJwtToken(accessToken);
            var jwtId = Guid.Parse(jwtToken.Id);
            var refreshTokenEntity = new Token
            {
                Id = Guid.NewGuid(),
                RefreshToken = refreshToken,
                JwtId = jwtId,
                UserId = user.Id,
                ExpiredAt = DateTime.UtcNow.AddDays(_jwtTokenSetting.RefreshTokenExpirationDays)
            };
            await _tokenRepo.Create(refreshTokenEntity);
            var tokenResponse = new TokenResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
            return tokenResponse;
        }

        public async Task<TokenResponseDto> RefreshAccessToken(string refreshToken)
        {
           
            var tokenInDb = await _tokenRepo.GetByRefreshToken(refreshToken);
            if (tokenInDb == null)
                throw new Exception("Invalid refresh token");

            if (tokenInDb.ExpiredAt < DateTime.UtcNow)
                throw new Exception("Refresh token has expired");
            var user = await _repo.GetUserById(tokenInDb.UserId);
            if (user == null)
                throw new Exception("User not found");
            var role = await _repo.GetRoleUserByEmail(user.Email);
            var authenticatedUser = new AuthenticatedUserModel
            {
                UserId = user.Id,
                UserName = user.Email,
                FirstName = user.Name,
                LastName = user.Name,
                Role = role ?? AuthenticatedUserModel.GuestRole
            };
            var (newAccessToken, newRefreshToken) = JwtUtil.CreateJwtAndRefreshToken(_jwtTokenSetting, authenticatedUser, role);
            var jwtHandler = new JwtSecurityTokenHandler();
            var newJwt = jwtHandler.ReadJwtToken(newAccessToken);
            var newJwtId = Guid.Parse(newJwt.Id);
            tokenInDb.RefreshToken = newRefreshToken;
            tokenInDb.JwtId = newJwtId;
            tokenInDb.ExpiredAt = DateTime.UtcNow.AddDays(_jwtTokenSetting.RefreshTokenExpirationDays);
            await _tokenRepo.Update(tokenInDb);
            return new TokenResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }
    }
}
