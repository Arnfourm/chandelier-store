using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using microservices.UserAPI.Domain.Interfaces.Services;
using microservices.UserAPI.Domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace microservices.UserAPI.Domain.Services
{
    public class JwtService : IJwtService
    {
        private readonly string _secretKey;
        private readonly int _accessTokenLifespan;
        private readonly int _refreshTokenLifespan;

        public JwtService(IConfiguration configuration)
        {
            _secretKey = configuration["JWTSecretKey"] ?? throw new ArgumentNullException("JWTSecretKey is null");
            _accessTokenLifespan = configuration.GetValue<int>("JWTLifespan");
            _refreshTokenLifespan = _accessTokenLifespan * 2;
        }

        public string GenerateAccessToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.GetId().ToString()),
                new Claim(ClaimTypes.Email, user.GetEmail()),
                new Claim(ClaimTypes.Name, user.GetName()),
                new Claim(ClaimTypes.Surname, user.GetSurname()),
                new Claim(ClaimTypes.Role, user.GetUserRole().ToString()),
                new Claim("tokenType", "access")
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddSeconds(_accessTokenLifespan),
                Issuer = "microservices.UserAPI",
                Audience = "microservices.Client",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public Guid? ValidateToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = "microservices.UserAPI",
                    ValidateAudience = true,
                    ValidAudience = "microservices.Client",
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken) validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

                return Guid.Parse(userId);
            }
            catch
            {
                return null;
            }
        }

        public (Guid userId, string email)? GetUserInfoFromToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);

                var userId = jwtToken.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
                var email = jwtToken.Claims.First(x => x.Type == ClaimTypes.Email).Value;

                return (Guid.Parse(userId), email);
            }
            catch
            {
                return null;
            }
        }
    }
}
