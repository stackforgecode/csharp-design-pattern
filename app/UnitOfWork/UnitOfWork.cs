// Classe Unity of Work
using Microsoft.EntityFrameworkCore;
using app.Database;
using app.Repositories.Implementations;

namespace app.UnitOfWork
{

    // /UnitOfWork/UnitOfWork.cs
    public class UnitOfWork : IDisposable
    {
        private AppDbContext context;
        private ProductRepository productRepository;

        public UnitOfWork() => context = new AppDbContext(); // Inicialize aqui o contexto do banco de dados
        

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}