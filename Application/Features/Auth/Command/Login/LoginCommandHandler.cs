using Application.Interfaces;
using BCrypt.Net;
using DeliveryApp.Application.Features.Auth.Common;
using DeliveryApp.Application.Interfaces;
using DeliveryApp.Domain.Settings;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Crypto.Generators;

namespace DeliveryApp.Application.Features.Auth.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, TokenResponseDto>
    {
        private readonly IAppDbContext _context;
        private readonly ITokenService _tokenService;
        private readonly TokenSettings _tokenSettings;
        private readonly ILogger<LoginCommandHandler> _logger;

        // حقن IOptions<TokenSettings> هنا للاستفادة من الـ Options Pattern في حساب تاريخ الانتهاء
        public LoginCommandHandler(
            IAppDbContext context,
            ITokenService tokenService,
            IOptions<TokenSettings> tokenSettings,
            ILogger<LoginCommandHandler> logger)
        {
            _context = context;
            _tokenService = tokenService;
            _tokenSettings = tokenSettings.Value; // استخراج القيمة
            _logger = logger;
        }

        public async Task<TokenResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting login for email: {Email}", request.Email);

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                _logger.LogWarning("Invalid login attempt for email: {Email}", request.Email);
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            var token = _tokenService.GenerateToken(user.Id, user.Email, user.Role.ToString());

            _logger.LogInformation("User with ID: {UserId} logged in successfully.", user.Id);

            return new TokenResponseDto
            {
                Token = token,
                Email = user.Email,
                Role = user.Role.ToString(),
            
            ExpiresAt = DateTime.UtcNow.AddDays(_tokenSettings.DurationInDays)// remove from heare add in AddJwtBearer in program class
            };
        }
    }
}