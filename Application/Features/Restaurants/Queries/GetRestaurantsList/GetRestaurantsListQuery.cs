using MediatR;

namespace DeliveryApp.Application.Features.Restaurants.Queries.GetRestaurantsList
{
    public record GetRestaurantsListQuery(int PageNumber = 1, int PageSize = 10) : IRequest<List<RestaurantListDto>>;
}