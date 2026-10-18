using DeliveryApp.DeliveryApp.Domain.Common;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Domain.Enums;

namespace DeliveryApp.DeliveryApp.Domain.Entities
{
    public class WalletTransaction : BaseEntity
    {
        public Guid WalletId { get; set; }
        public Wallet wallet { get; set; } = null!;

        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
       // public bool IsDeposit { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
