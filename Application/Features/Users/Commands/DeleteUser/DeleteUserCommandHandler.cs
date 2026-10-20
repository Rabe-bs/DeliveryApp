using Application.Interfaces;
using DeliveryApp.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IAppDbContext _context;

        public DeleteUserCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

            if (user == null)
            {
                throw new KeyNotFoundException("this user not funde ");
            }

            // الحذف الفعلي (Hard Delete)
            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}