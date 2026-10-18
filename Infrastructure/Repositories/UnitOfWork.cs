using DeliveryApp.Application.Interfaces;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Infrastructure.Persistence;

namespace DeliveryApp.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IWalletRepository Wallets { get; }
        public IWalletTransactionRepository WalletTransactions { get; }
        public IOrderRepository Orders { get; }

        public IGenericRepository<DeliveryTrip> DeliveryTrips => throw new NotImplementedException();

        public UnitOfWork(
            AppDbContext context,
            IWalletRepository wallets,
            IWalletTransactionRepository walletTransactions,
            IOrderRepository orders)
        {
            _context = context;
            Wallets = wallets;
            WalletTransactions = walletTransactions;
            Orders = orders;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}