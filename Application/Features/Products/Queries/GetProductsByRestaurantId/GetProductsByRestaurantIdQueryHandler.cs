using Application.Interfaces;
using DeliveryApp.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Features.Products.Queries.GetProductsByRestaurantId
{
    public class GetProductsByRestaurantIdQueryHandler : IRequestHandler<GetProductsByRestaurantIdQuery, List<ProductDto>>
    {
        private readonly IAppDbContext _context;

        public GetProductsByRestaurantIdQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductDto>> Handle(GetProductsByRestaurantIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Products
                .AsNoTracking()
                .Where(p => p.RestaurantId == request.RestaurantId)
                .Select(p => new ProductDto(
                    p.Id,
                    p.Name,
                    p.Description,
                    p.Price,
                    p.IsAvailable
                ))
                .ToListAsync(cancellationToken);
        }
    }
}