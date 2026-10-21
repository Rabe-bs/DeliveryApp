using Application.Interfaces;
using DeliveryApp.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly IAppDbContext _context;

        public DeleteProductCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (product == null)
            {
                throw new KeyNotFoundException("Product not exist.");
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}