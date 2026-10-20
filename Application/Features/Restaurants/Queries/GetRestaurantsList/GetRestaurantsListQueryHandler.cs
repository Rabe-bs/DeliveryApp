using Application.Interfaces;
using DeliveryApp.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Features.Restaurants.Queries.GetRestaurantsList
{
    public class GetRestaurantsListQueryHandler : IRequestHandler<GetRestaurantsListQuery, List<RestaurantListDto>>
    {
        private readonly IAppDbContext _context;

        public GetRestaurantsListQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<RestaurantListDto>> Handle(GetRestaurantsListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Restaurants
                .AsNoTracking()
                .OrderBy(r => r.Name)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(r => new RestaurantListDto(
                    r.Id,
                    r.Name,
                    r.Address,
                    r.PhoneNumber
                ))
                .ToListAsync(cancellationToken);
        }
    }
}