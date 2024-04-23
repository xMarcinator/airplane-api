namespace AirplaneAPI.Database.Repositories.Interface;

public interface IRepository<T,TK>
{
    /// <summary>
    /// Read all entities
    /// </summary>
    Task<IEnumerable<T>> ReadAllAsync();
    /// <summary>
    /// Read entity by criteria
    /// </summary>
    /// <param name="criteria">Primary key of entity</param>
    /// <returns>The stored entity</returns>
    Task<T?> ReadAsync(TK criteria);
    /// <summary>
    /// Create entity in database
    /// </summary>
    /// <param name="entity">Entity to create</param>
    /// <returns>Created Entity</returns>
    Task<T> CreateAsync(T entity);
    /// <summary>
    /// Update entity in database
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>Updated Entity</returns>
    Task<T> UpdateAsync(T entity);
    /// <summary>
    /// Delete entity from database
    /// </summary>
    /// <param name="entity">Entity to be deleted</param>
    /// <returns>Deleted Entity</returns>
    Task<T> DeleteAsync(T entity);
    /// <summary>
    /// Delete entity from database by criteria
    /// </summary>
    /// <param name="criteria">Primary key of Entity</param>
    /// <returns>Deleted Entity</returns>
    Task<T?> DeleteAsync(TK criteria);
}