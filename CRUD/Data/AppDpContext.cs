using Microsoft.EntityFrameworkCore;
namespace CRUD.Data
{
    public class AppDpContext : DbContext
    {

         public DbSet<Product> Products { get; set; }
        public AppDpContext(DbContextOptions options) : base(options) 
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().ToTable("Products");

        }





    }
}
