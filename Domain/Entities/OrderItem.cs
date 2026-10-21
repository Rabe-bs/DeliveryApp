using DeliveryApp.DeliveryApp.Domain.Common;
using DeliveryApp.DeliveryApp.Domain.Entities;

namespace DeliveryApp.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public Guid OrderId { get; private set; }
        public Order Order { get; private set; } = null!;

        public Guid ProductId { get; private set; }
        public Product Product { get; private set; } = null!;

        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal TotalPrice => Quantity * UnitPrice;

        private OrderItem() { }

        public OrderItem(Guid productId, int quantity, decimal unitPrice)
        {
            if (quantity <= 0) throw new ArgumentException("Quantity must be at least 1.");
            if (unitPrice <= 0) throw new ArgumentException("UnitPrice must be greater than zero.");

            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
}