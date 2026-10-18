using MediatR;

namespace DeliveryApp.Application.Features.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand(
        Guid CustomerId,
        Guid RestaurantId,
        decimal TotalAmount,
        string DeliveryAddress
    ) : IRequest<Guid>;
}