using DeliveryApp.Application.Interfaces;
using DeliveryApp.Domain.Settings;
using DeliveryApp.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DeliveryApp.Infrastructure.Sirvece
{
    public class TokenService : ITokenService
    {
        private readonly TokenSettings _tokenSettings;
        private readonly IConfiguration _configuration;
        private readonly ILogger<TokenService> _logger;

        public TokenService(IConfiguration configuration, ILogger<TokenService> logger, IOptions<TokenSettings> options)
        {
            _configuration = configuration;
            _logger = logger;
            _tokenSettings = options.Value;

            // حماية للتأكد من قراءة المفتاح وعدم تركها فارغة
            if (string.IsNullOrEmpty(_tokenSettings.Secret))
            {
                throw new InvalidOperationException("JwtSettings:Secret is missing or empty in configuration.");
            }
        }

        public string GenerateToken(Guid userId, string email, string role)
        {
            _logger.LogInformation("Generating JWT token for user ID: {UserId} with role: {Role}", userId, role);

            var key = Encoding.UTF8.GetBytes(_tokenSettings.Secret);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_tokenSettings.ExpiryMinutes), 
                Issuer = _tokenSettings.Issuer,
                Audience = _tokenSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            _logger.LogInformation("JWT token successfully generated for user ID: {UserId}", userId);
            return tokenHandler.WriteToken(token);
        }
    }
}