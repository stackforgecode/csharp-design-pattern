# 👨🏽‍💻PADRÕES DE PROJETO COM C#🖥

O padrão Unity of Work (Unidade de Trabalho) é um padrão de design aplicado ao desenvolvimento de software, especificamente no contexto da linguagem de programação C#. Ele visa facilitar o gerenciamento de transações e operações de banco de dados em um aplicativo.

O padrão Unity of Work é usado em conjunto com o padrão Repository (Repositório), que fornece uma abstração para a camada de acesso a dados. O objetivo principal do padrão Unity of Work é coordenar várias operações de banco de dados em uma única transação, garantindo a consistência e a atomicidade das operações.

Aqui está um exemplo de implementação do padrão Unity of Work em C#:

```csharp
// Classe de Entidade
// /Models/Product.cs
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

// /Repositories/Interfaces/IRepository.cs
public interface IRepository<T>
{
    void Add(T entity);
    void Remove(T entity);
    T GetById(int id);
    IEnumerable<T> GetAll();
}

// /Repositories/Implementations/ProductRepository.cs
public class ProductRepository : IRepository<Product>
{
    private DbContext context;

    public ProductRepository(DbContext dbContext)
    {
        context = dbContext;
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

// /UnitOfWork/UnitOfWork.cs
public class UnitOfWork : IDisposable
{
    private DbContext context;
    private ProductRepository productRepository;

    public UnitOfWork()
    {
        context = new DbContext(); // Inicialize aqui o contexto do banco de dados
    }

    public ProductRepository ProductRepository
    {
        get
        {
            if (productRepository == null)
            {
                productRepository = new ProductRepository(context);
            }
            return productRepository;
        }
    }

    public void Save()
    {
        context.SaveChanges();
    }

    public void Dispose()
    {
        context.Dispose();
    }
}

// /Database/AppDbContext.cs
public class AppDbContext : DbContext
{
    // Configurações do DbContext
}

// Exemplo de Uso
using (var unitOfWork = new UnitOfWork())
{
    var productRepository = unitOfWork.ProductRepository;

    var product1 = new Product { Name = "Product 1", Price = 10.99m };
    productRepository.Add(product1);

    var product2 = new Product { Name = "Product 2", Price = 19.99m };
    productRepository.Add(product2);

    unitOfWork.Save(); // Salva as alterações no banco de dados
}

```

Neste exemplo, temos a classe Product como uma entidade de exemplo. O ProductRepository implementa a interface IRepository<Product> e é responsável por interagir com o banco de dados para operações relacionadas aos produtos.

A classe UnitOfWork coordena as operações em um único contexto de banco de dados. Ela encapsula o contexto do banco de dados e fornece acesso ao repositório de produtos (ProductRepository). Ao chamar o método Save, as alterações feitas no contexto são persistidas no banco de dados.

O padrão Unity of Work ajuda a simplificar a implementação de transações e operações de banco de dados em um aplicativo C#, garantindo a consistência dos dados e melhorando a eficiência do acesso aos dados.
  
----

## Referências

Aqui estão algumas referências que você pode utilizar para obter mais informações sobre o padrão Unity of Work e sua implementação em C#:

1. Documentação oficial do Entity Framework Core:
   - [Página oficial do Unity of Work](https://docs.microsoft.com/en-us/ef/core/saving/related-data)
   - [Página oficial do Repository pattern](https://docs.microsoft.com/en-us/ef/core/querying/related-data)

2. Livros:
   - "Design Patterns: Elements of Reusable Object-Oriented Software" por Erich Gamma, Richard Helm, Ralph Johnson e John Vlissides.
   - "Patterns of Enterprise Application Architecture" por Martin Fowler.

3. Artigos e tutoriais:
   - [Implementing the Unit of Work and Repository Patterns in an ASP.NET MVC Application](https://ardalis.com/asp-net-mvc-tutorial-unit-of-work-repository-patterns) por Steve Smith.
   - [Implementing Repository and Unit of Work Patterns in ASP.NET MVC Applications](https://docs.microsoft.com/en-us/previous-versions/msp-n-p/ff649690(v=pandp.10)) por Tom Dykstra.

Essas referências fornecerão uma base sólida para compreender o padrão Unity of Work e sua aplicação em C#. Além disso, explorar a documentação oficial do Entity Framework Core é uma ótima maneira de aprender mais sobre a implementação prática desse padrão.  

