
using DeliveryApp.Infrastructure.Persistence;
using DeliveryApp.DeliveryApp.Domain.Entities;
using DeliveryApp.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Infrastructure.Repositories;
public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context)
    {
    }



    public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(Guid customerId)
    {
        return await _dbSet
            .Where(o => o.CustomerId == customerId)
            .Include(o => o.Restaurant)
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetOrdersByRestaurantIdAsync(Guid restaurantId)
    {
        return await _dbSet
            .Where(o => o.RestaurantId == restaurantId)
            .ToListAsync();
    }
}