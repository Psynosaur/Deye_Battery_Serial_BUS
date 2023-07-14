namespace CAN2JSON.Data.Repository;

public interface IRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Get all entities as IQueryable
    /// </summary>
    /// <returns></returns>
    IQueryable<TEntity> GetAll();
        
    /// <summary>
    /// Adds an entity 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<TEntity> AddAsync(TEntity entity);
        
    /// <summary>
    /// Updates and entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<TEntity> UpdateAsync(TEntity entity);
        
    /// <summary>
    /// Deletes an entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<TEntity> DeleteAsync(TEntity entity);
}