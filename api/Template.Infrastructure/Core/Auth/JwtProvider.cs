using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Template.Application.Core.Abstractions.Auth;
using Template.Application.Core.Abstractions.Commons;
using Template.Domain.Entities;
using Template.Infrastructure.Core.Auth.Settings;

namespace Template.Infrastructure.Core.Auth
{
    internal sealed class JwtProvider(
        IOptions<JwtSettings> jwtOptions,
        IDateTime dateTime) : IJwtProvider
    {
        private readonly JwtSettings _jwtSettings = jwtOptions.Value;
        private readonly IDateTime _dateTime = dateTime;
        public const string UserIdClaimName = "userId";

        public (string token, DateTime tokenExpirationTime) Create(User user, bool refreshToken = false)
        {
            var key = !refreshToken ? _jwtSettings.SecurityAccessTokenKey : _jwtSettings.SecurityRefreshTokenKey;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            Claim[] claims =
            [
                new Claim(UserIdClaimName, user.Id.ToString()),
                new Claim("email", user.Email),
                new Claim("name", user.FullName)
            ];

            var expiration = !refreshToken ? _jwtSettings.TokenExpirationInMinutes : _jwtSettings.TokenRefreshExpirationInMinutes;
            DateTime tokenExpirationTime = _dateTime.UtcNow.AddMinutes(expiration);

            var token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                null,
                tokenExpirationTime,
                signingCredentials);

            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return (tokenValue, tokenExpirationTime);
        }


    }
}
