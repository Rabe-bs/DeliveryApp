using DeliveryApp.DeliveryApp.Domain.Common;
using DeliveryApp.DeliveryApp.Domain.Enums;
using DeliveryApp.Domain.Entities;

namespace DeliveryApp.DeliveryApp.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FullName { get; set; } =string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public UserRole Role { get; set; }

        public bool IsActive { get; set; } = true;
        public Wallet? Wallet { get; set; }
        public ICollection<Order> orders { get; set; } = new List<Order>();
    }
}
