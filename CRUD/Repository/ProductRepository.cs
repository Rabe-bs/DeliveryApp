using CRUD.Data;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDpContext _context;

        public ProductRepository(AppDpContext context)
        {
            _context = context;

        }

        public async Task<IEnumerable<Product>> GetAll() => await _context.Products.ToListAsync();
        public async Task<Product> GetById(int id) => await _context.Products.FindAsync(id);
        public async Task Add(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var product = await GetById(id);

            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();

        }
    }
}
