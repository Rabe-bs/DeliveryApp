using Application.Interfaces;
using DeliveryApp.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Features.Users.Queries.GetUsersList
{
    public class GetUsersListQueryHandler : IRequestHandler<GetUsersListQuery, List<UserListDto>>
    {
        private readonly IAppDbContext _context;

        public GetUsersListQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserListDto>> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AsNoTracking()
                .OrderBy(u => u.FullName)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(u => new UserListDto(
                    u.Id,
                    u.FullName,
                    u.Email,
                    u.PhoneNumber,
                    u.Role,
                    u.IsActive
                ))
                .ToListAsync(cancellationToken);
        }
    }
}