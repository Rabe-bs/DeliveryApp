using DeliveryApp.DeliveryApp.Domain.Common;
using DeliveryApp.DeliveryApp.Domain.Enums;
namespace DeliveryApp.DeliveryApp.Domain.Entities
{
    public class Order :BaseEntity
    {
        public Guid CustomerId { get; set; }
        public User Customer { get; set; } = null!;

        public Guid RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; } = null!;

        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public Guid? DeliveryGuyId { get; set; }
        public string DeliveryAddress { get; set; } = string.Empty;

        public string? CancellationReason { get; set; }
        public bool IsRefunded { get; set; } = false;

    }
}
