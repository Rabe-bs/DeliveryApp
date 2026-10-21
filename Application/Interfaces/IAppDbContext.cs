using DeliveryApp.DeliveryApp.Domain.Entities;
using DeliveryApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Restaurant> Restaurants { get; }
        DbSet<Order> Orders { get; }
        DbSet<DeliveryTrip> DeliveryTrips { get; }
        DbSet<Wallet> Wallets { get; }
        DbSet<WalletTransaction> WalletTransactions { get; }
        DbSet<Product> Products { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
