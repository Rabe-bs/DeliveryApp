using Application.Interfaces;
using DeliveryApp.Application.Features.Restaurants.Queries.GetRestaurantById;
using DeliveryApp.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Restaurants.Queries.GetRestaurantById
{
    public class GetRestaurantByIdQueryHandler : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto>
    {
        private readonly IAppDbContext _context;

        public GetRestaurantByIdQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<RestaurantDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
        {
            var restaurant = await _context.Restaurants
                .AsNoTracking()
                .Where(r => r.Id == request.Id)
                .Select(r => new RestaurantDto(
                    r.Id,
                    r.Name,
                    r.Address,
                    r.PhoneNumber
                ))
                .FirstOrDefaultAsync(cancellationToken);

            if (restaurant == null)
            {
                throw new KeyNotFoundException("restaurent not fund .");
            }

            return restaurant;
        }
    }
}