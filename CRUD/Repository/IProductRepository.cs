using CRUD.Data;

namespace CRUD.Repository
{
    public interface IProductRepository
    {
     public  Task<IEnumerable<Product>> GetAll();
      public  Task<Product> GetById(int id);
      public  Task Add(Product product);
       public void Update(Product product);
       public Task Delete(int id);
        

    }
}
