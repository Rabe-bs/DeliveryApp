using Application.Interfaces;
using DeliveryApp.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Features.Users.Commands.ToggleUserStatus
{
    public class ToggleUserStatusCommandHandler : IRequestHandler<ToggleUserStatusCommand, bool>
    {
        private readonly IAppDbContext _context;

        public ToggleUserStatusCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(ToggleUserStatusCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

            if (user == null)
            {
                throw new KeyNotFoundException("المستخدم غير موجود.");
            }

            // هنا اعكس حاله التفعيل 
            user.IsActive = !user.IsActive;

            await _context.SaveChangesAsync(cancellationToken);

            return user.IsActive;
        }
    }
}