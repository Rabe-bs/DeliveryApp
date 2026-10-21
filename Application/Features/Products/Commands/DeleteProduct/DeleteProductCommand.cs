using MediatR;

namespace DeliveryApp.Application.Features.Products.Commands.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : IRequest<Unit>;
}