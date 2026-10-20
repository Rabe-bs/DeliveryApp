using Application.Interfaces;
using DeliveryApp.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Features.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IAppDbContext _context;

        public GetUserByIdQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .AsNoTracking()
                .Where(u => u.Id == request.Id)
                .Select(u => new UserDto(
                    u.Id,
                    u.FullName,
                    u.Email,
                    u.PhoneNumber,
                    u.Role,
                    u.IsActive
                ))
                .FirstOrDefaultAsync(cancellationToken);

            if (user == null)
            {
                throw new KeyNotFoundException("المستخدم غير موجود.");
            }

            return user;
        }
    }
}