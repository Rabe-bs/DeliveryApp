using DeliveryApp.Domain.Entities;

namespace DeliveryApp.Application.Interfaces
{
    public interface IWalletRepository : IGenericRepository<Wallet>
    {
        Task<Wallet?> GetByUserIdAsync(Guid userId);
    }
}
