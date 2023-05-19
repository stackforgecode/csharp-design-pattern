
using app.Database;
using Microsoft.EntityFrameworkCore;

namespace app.Repositories.Implementations
{
    // Implementação do Repositório
    public class ProductRepository : IRepository<Product>
    {
        private AppDbContext context;

        public ProductRepository(DbContext dbContext)
        {
            //
        }

        public void Add(Product entity)
        {
            context.Set<Product>().Add(entity);
        }

        public void Remove(Product entity)
        {
            context.Set<Product>().Remove(entity);
        }

        public Product GetById(int id)
        {
            return context.Set<Product>().Find(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return context.Set<Product>().ToList();
        }
    }
}