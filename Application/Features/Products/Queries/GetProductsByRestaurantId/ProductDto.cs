namespace DeliveryApp.Application.Features.Products.Queries.GetProductsByRestaurantId
{
    public record ProductDto(
        Guid Id,
        string Name,
        string Description,
        decimal Price,
        bool IsAvailable
    );
}