namespace AirplaneAPI.Database.Repositories.Interface;

public interface IRepository<T,TK>
{
    Task<IEnumerable<T>> ReadAllAsync();
    
    Task<T?> ReadAsync(TK criteria);
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(T entity);
    Task<T?> DeleteAsync(TK criteria);
}