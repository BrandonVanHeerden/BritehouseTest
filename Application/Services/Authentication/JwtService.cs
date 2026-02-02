using Domain.Abstraction.Application;
using Domain.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services.Authentication
{
    public class JwtService:IJwtService
    {
        private readonly JWTConfiguration _jwtConfiguration;

        public JwtService(JWTConfiguration jwtConfiguration)
        {
            _jwtConfiguration = jwtConfiguration;
        }

        public IEnumerable<Claim> GetClaims(string userId, string account, bool accountActive, IEnumerable<string> roles = null)
        {
            var claims = new List<Claim> {
            new Claim("UserId",userId),
            new Claim("APP_UUID",_jwtConfiguration.APP_UUID.ToString()),
            new Claim("Account",account),
            new Claim( JwtRegisteredClaimNames.Aud, _jwtConfiguration.Audience),
            new Claim( JwtRegisteredClaimNames.Iss, _jwtConfiguration.Issuer),
            new Claim("AccountActive",accountActive.ToString())
        };

            if (roles != null && roles.Any())
            {
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            return claims;
        }

        public Task<string> GenerateAsync(IEnumerable<Claim> claims)
        {
            int? expiryMinutes = 60;
            DateTime? expires = null;
            if (expiryMinutes != null)
                expires = new DateTimeOffset(DateTime.Now.AddMinutes((int)expiryMinutes)).DateTime;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _jwtConfiguration.Issuer,
                Audience = _jwtConfiguration.Audience
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescription);
            return Task.FromResult(tokenHandler.WriteToken(token));
        }

        public (string refreshToken, DateTime expireDateTime) GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                var refreshToken = Convert.ToBase64String(randomNumber);
                var expireDateTime = DateTime.UtcNow.AddDays(_jwtConfiguration.RefreshTokenExpirationTimeInDays);
                return (refreshToken, expireDateTime);
            }
        }
    }
}
