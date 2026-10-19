using DeliveryApp.Application.Interfaces;
using DeliveryApp.Infrastructure.Settings;
using Infrastructure;
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
        private TokenSettings TokenSettings = new TokenSettings();
        private readonly IConfiguration _configuration; // Option Pattern 
        private readonly ILogger<TokenService> _logger;

        public TokenService(IConfiguration configuration, ILogger<TokenService> logger, IOptions<TokenSettings> options)
        {
            _configuration = configuration;
            _logger = logger;
            TokenSettings = options.Value;
        }

        public string GenerateToken(Guid userId, string email, string role)
        {
            _logger.LogInformation("Generating JWT token for user ID: {UserId} with role: {Role}", userId, role);

            // استخدام الـ Options Pattern مباشرة بدون الحاجة لقراءة الـ Configuration يدوياً داخل الدالة
            var key = Encoding.ASCII.GetBytes(TokenSettings.Secret!);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(TokenSettings.DurationInDays),
                Issuer = TokenSettings.Issuer,
                Audience = TokenSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            _logger.LogInformation("JWT token successfully generated for user ID: {UserId}", userId);
            return tokenHandler.WriteToken(token);
        }
    }
}