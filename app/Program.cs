using app.Database;
using app.Repositories.Implementations;
using app.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Configuração do banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer("YourConnectionString")); // Substitua "YourConnectionString" pela sua string de conexão real

// Registro dos serviços
builder.Services.AddTransient<IRepository<Product>, ProductRepository>();
builder.Services.AddTransient<UnitOfWork>();

var app = builder.Build();

// Exemplo de uso
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated(); // Cria o banco de dados, se não existir

    var unitOfWork = services.GetRequiredService<UnitOfWork>();
    var productRepository = services.GetRequiredService<IRepository<Product>>();

    var product1 = new Product { Name = "Product 1", Price = 10.99m };
    productRepository.Add(product1);

    var product2 = new Product { Name = "Product 2", Price = 19.99m };
    productRepository.Add(product2);

    unitOfWork.Save(); // Salva as alterações no banco de dados
}

app.Run();

