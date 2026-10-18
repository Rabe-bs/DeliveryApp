using DeliveryApp.DeliveryApp.Domain.Common;

namespace DeliveryApp.DeliveryApp.Domain.Entities
{
    public class Restaurant :BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public ICollection<Order> Orders { get; set; } = new List<Order>();

    }
}
