using MediatR;

namespace DeliveryApp.Application.Features.Restaurants.Commands.CreateRestaurant
{
    public record CreateRestaurantCommand(
        string Name,
        string Address,
        string PhoneNumber
    ) : IRequest<Guid>;
}