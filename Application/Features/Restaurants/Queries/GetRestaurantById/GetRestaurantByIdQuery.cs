using MediatR;

namespace DeliveryApp.Application.Features.Restaurants.Queries.GetRestaurantById
{
    public record GetRestaurantByIdQuery(Guid Id) : IRequest<RestaurantDto>;
}