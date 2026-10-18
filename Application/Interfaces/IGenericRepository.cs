namespace DeliveryApp.Application.Interfaces
{
    using System.Linq.Expressions;
    public interface IGenericRepository<T> where T : class
    {
        //      CancellationToken
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(T entity, CancellationToken cancellationToken = default);
        Task Update(T entity, CancellationToken cancellationToken = default);
        Task Delete(T entity, CancellationToken cancellationToken = default);
    }
}
