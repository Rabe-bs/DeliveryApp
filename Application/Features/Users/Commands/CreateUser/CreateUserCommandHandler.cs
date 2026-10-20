using Application.Interfaces;
using DeliveryApp.Application.Interfaces;
using DeliveryApp.DeliveryApp.Domain.Entities;
using DeliveryApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IAppDbContext _context;

        public CreateUserCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var emailExists = await _context.Users
                .AnyAsync(u => u.Email == request.Email, cancellationToken);

            if (emailExists)
            {
                throw new InvalidOperationException("Email is used alredy .");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                PasswordHash = passwordHash,
                Role = request.Role,
                IsActive = true
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}