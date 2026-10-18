using DeliveryApp.DeliveryApp.Domain.Entities;

namespace DeliveryApp.Application.Interfaces
{
    using DeliveryApp.Domain.Entities;
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(Guid customerId);
        Task<IEnumerable<Order>> GetOrdersByRestaurantIdAsync(Guid restaurantId);

    }
}
