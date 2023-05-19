// Interface do Repositório
public interface IRepository<T>
{
    void Add(T entity);
    void Remove(T entity);
    T GetById(int id);
    IEnumerable<T> GetAll();
}