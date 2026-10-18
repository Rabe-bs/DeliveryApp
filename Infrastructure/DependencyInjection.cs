using Application.Interfaces;
using DeliveryApp.Application.Interfaces;
using DeliveryApp.Infrastructure.Persistence;
using DeliveryApp.Infrastructure.Repositories;
using DeliveryApp.Infrastructure.Sirvece;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DeliveryApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // 1. Database Context
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // 2. Interfaces Bindings
            services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());

            // 3. Repositories & UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<IWalletTransactionRepository, WalletTransactionRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // 4. Infrastructure Services
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}