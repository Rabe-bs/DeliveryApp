namespace DeliveryApp.Application.Features.Restaurants.Queries.GetRestaurantsList
{
    public record RestaurantListDto(
        Guid Id,
        string Name,
        string Address,
        string PhoneNumber
    );
}