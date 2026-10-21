using MediatR;

namespace DeliveryApp.Application.Features.Products.Commands.CreateProduct
{
    public record CreateProductCommand(
        Guid RestaurantId,
        string Name,
        string Description,
        decimal Price
    ) : IRequest<Guid>;
}