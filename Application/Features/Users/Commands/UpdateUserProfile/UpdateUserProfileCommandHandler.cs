using Application.Interfaces;
using DeliveryApp.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Features.Users.Commands.UpdateUserProfile
{
    public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, bool>
    {
        private readonly IAppDbContext _context;

        public UpdateUserProfileCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

            if (user == null)
            {
                throw new KeyNotFoundException("user not exist .");
            }

            user.FullName = request.FullName;
            user.PhoneNumber = request.PhoneNumber;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}