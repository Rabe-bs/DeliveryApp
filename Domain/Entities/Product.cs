using DeliveryApp.DeliveryApp.Domain.Common;
using DeliveryApp.DeliveryApp.Domain.Entities;

namespace DeliveryApp.Domain.Entities
{
    public class Product : BaseEntity
    {
        public Guid RestaurantId { get; private set; }
        public Restaurant Restaurant { get; private set; } = null!;

        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public bool IsAvailable { get; private set; } = true;

        // حماية قائمة عناصر الطلبات لكي لا تُعدل عشوائياً من خارج الكيان
        private readonly List<OrderItem> _orderItems = new();
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

        // 1. Constructor خاص بـ Entity Framework Core لكي يتمكن من قراءة البيانات
        private Product() { }

        // 2. Constructor أساسي لإنشاء منتج جديد مع التحقق من الشروط (Validation)
        public Product(Guid restaurantId, string name, string description, decimal price)
        {
            if (price <= 0)
                throw new ArgumentException("Price must be greater than zero.");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name cannot be empty.");

            RestaurantId = restaurantId;
            Name = name;
            Description = description;
            Price = price;
            IsAvailable = true;
        }

        public void UpdateDetails(string name, string description, decimal price)
        {
            if (price <= 0)
                throw new ArgumentException("Price must be greater than zero.");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name cannot be empty.");

            Name = name;
            Description = description;
            Price = price;
        }

        public void ToggleAvailability(bool isAvailable)
        {
            IsAvailable = isAvailable;
        }
    }
}