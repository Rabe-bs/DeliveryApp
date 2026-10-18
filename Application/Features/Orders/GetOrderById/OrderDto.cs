using DeliveryApp.DeliveryApp.Domain.Enums;

namespace DeliveryApp.Application.Features.Orders.Queries.GetOrderById
{
    public record OrderDto(
        Guid Id,
        Guid CustomerId,
        Guid RestaurantId,
        decimal TotalAmount,
        OrderStatus Status,
        Guid? DeliveryGuyId,
        string DeliveryAddress,
        string? CancellationReason,
        bool IsRefunded,
        DateTime CreatedAt
    );
}