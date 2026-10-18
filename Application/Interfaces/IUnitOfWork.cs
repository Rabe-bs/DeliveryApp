using DeliveryApp.Domain.Entities;

namespace DeliveryApp.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IWalletRepository Wallets { get; }
    IWalletTransactionRepository WalletTransactions { get; }
    IOrderRepository Orders { get; }
    IGenericRepository<DeliveryTrip> DeliveryTrips { get; }
    Task<int> CompleteAsync();
    public  Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

}