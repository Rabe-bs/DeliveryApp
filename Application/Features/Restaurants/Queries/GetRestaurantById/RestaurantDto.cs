namespace DeliveryApp.Application.Features.Restaurants.Queries.GetRestaurantById
{
    public record RestaurantDto(
        Guid Id,
        string Name,
        string Address,
        string PhoneNumber
    );
}