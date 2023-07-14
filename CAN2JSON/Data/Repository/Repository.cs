using CAN2JSON.Data.Context;

namespace CAN2JSON.Data.Repository;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
{
    protected readonly Can2JsonContext Can2JsonContext;

    public Repository(Can2JsonContext can2JsonContext)
    {
        Can2JsonContext = can2JsonContext;
    }

    public IQueryable<TEntity> GetAll()
    {
        try
        {
            return Can2JsonContext.Set<TEntity>();
        }
        catch (Exception ex)
        {
            throw new Exception($"Couldn't retrieve entities: {ex.Message}");
        }
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");

        try
        {
            await Can2JsonContext.AddAsync(entity);
            await Can2JsonContext.SaveChangesAsync();

            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception($"{nameof(entity)} could not be saved: {ex.Message}");
        }
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException($"{nameof(UpdateAsync)} entity must not be null");

        try
        {
            Can2JsonContext.Update(entity);
            await Can2JsonContext.SaveChangesAsync();

            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception($"{nameof(entity)} could not be updated: {ex.Message}");
        }
    }
        
    public async Task<TEntity> DeleteAsync(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");

        try
        {
            Can2JsonContext.Remove(entity);
            await Can2JsonContext.SaveChangesAsync();

            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception($"{nameof(entity)} could not be deleted: {ex.Message}");
        }
    }
}