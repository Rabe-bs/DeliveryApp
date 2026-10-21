using MediatR;

namespace DeliveryApp.Application.Features.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand(
        Guid CustomerId,
        Guid RestaurantId,
        string DeliveryAddress,
        decimal TotalAmount,
        List<OrderItemDto> Items
    ) : IRequest<Guid>;

    public record OrderItemDto(
        Guid ProductId,
        int Quantity,
        decimal UnitPrice
    );
}