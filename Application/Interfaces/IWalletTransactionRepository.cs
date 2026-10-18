using DeliveryApp.DeliveryApp.Domain.Entities;

namespace DeliveryApp.Application.Interfaces
{
    public interface IWalletTransactionRepository : IGenericRepository<WalletTransaction>
    {
        Task<IEnumerable<WalletTransaction>> GetByWalletIdAsync(Guid walletId);
    }
}
