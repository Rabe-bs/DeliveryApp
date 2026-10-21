using Application.Interfaces;
using DeliveryApp.Application.Interfaces;
using DeliveryApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IAppDbContext _context;

        public CreateProductCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var restaurantExists = await _context.Restaurants
                .AnyAsync(r => r.Id == request.RestaurantId, cancellationToken);

            if (!restaurantExists)
            {
                throw new KeyNotFoundException($"Restaurant with ID {request.RestaurantId} was not found.");
            }

            var product = new Product(
                request.RestaurantId,
                request.Name,
                request.Description,
                request.Price
            );

            _context.Products.Add(product);
            await _context.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }
}