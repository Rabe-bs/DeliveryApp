using MediatR;

namespace DeliveryApp.Application.Features.Products.Queries.GetProductsByRestaurantId
{
    public record GetProductsByRestaurantIdQuery(Guid RestaurantId) : IRequest<List<ProductDto>>;
}