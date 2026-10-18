using DeliveryApp.Application.Interfaces;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Infrastructure.Repositories
{
    public class WalletRepository : GenericRepository<Wallet>, IWalletRepository
    {
        public WalletRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Wallet?> GetByUserIdAsync(Guid userId)
        {
            return await _dbSet
                .Include(w => w.WalletTransactions)
                .FirstOrDefaultAsync(w => w.UserId == userId);
        }
    }
}