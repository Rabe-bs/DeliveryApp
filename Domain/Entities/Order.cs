using DeliveryApp.DeliveryApp.Domain.Common;
using DeliveryApp.DeliveryApp.Domain.Enums;
using DeliveryApp.Domain.Entities;

namespace DeliveryApp.DeliveryApp.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Guid CustomerId { get; private set; }
        public User Customer { get; private set; } = null!;

        public Guid RestaurantId { get; private set; }
        public Restaurant Restaurant { get; private set; } = null!;

        public Guid? DeliveryGuyId { get; private set; }
        public User? DeliveryGuy { get; private set; }

        public decimal TotalAmount { get; private set; }
        public OrderStatus Status { get; private set; } = OrderStatus.Pending;

        public string DeliveryAddress { get; private set; } = string.Empty;
        public string? CancellationReason { get; private set; }
        public bool IsRefunded { get; private set; } = false;

        private readonly List<OrderItem> _orderItems = new();
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

        private Order() { }

        public Order(Guid customerId, Guid restaurantId, string deliveryAddress)
        {
            CustomerId = customerId;
            RestaurantId = restaurantId;
            DeliveryAddress = deliveryAddress;
            Status = OrderStatus.Pending;
        }

        public void AddItem(Guid productId, int quantity, decimal unitPrice)
        {
            if (Status != OrderStatus.Pending)
                throw new InvalidOperationException("Cannot modify items after order is processed.");

            _orderItems.Add(new OrderItem(productId, quantity, unitPrice));
            RecalculateTotalAmount();
        }

        public void AssignDeliveryGuy(Guid deliveryGuyId)
        {
            DeliveryGuyId = deliveryGuyId;
            Status = OrderStatus.OutForDelivery; 
        }

        public void CancelOrder(string reason, bool isRefunded)
        {
            Status = OrderStatus.CancelledByCustomer; 
            CancellationReason = reason;
            IsRefunded = isRefunded;
        }

        private void RecalculateTotalAmount()
        {
            TotalAmount = _orderItems.Sum(item => item.TotalPrice);
        }

        public void UpdateStatus(OrderStatus newStatus)
        {
            Status = newStatus;
        }
    }
}