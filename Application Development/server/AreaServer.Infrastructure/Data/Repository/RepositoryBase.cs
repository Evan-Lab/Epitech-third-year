using AreaServer.Infrastructure.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    private readonly DbContext dbContext;

    public RepositoryBase(DbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    //---


    public virtual async Task<T?> GetAsync(Expression<Func<T, bool>> expr, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<T>().Where(expr).FirstOrDefaultAsync(cancellationToken);
    }

    public virtual async Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull
    {
        return await dbContext.Set<T>().FindAsync(new object[] { id }, cancellationToken: cancellationToken);
    }


    //---


    public virtual async Task<List<T>?> ListAsync(Expression<Func<T, bool>> expr, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<T>().Where(expr).ToListAsync(cancellationToken);
    }

    public virtual async Task<List<T>?> ListAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<T>().ToListAsync(cancellationToken);
    }

    //---

    public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        dbContext.Set<T>().Add(entity);

        await SaveChangesAsync(cancellationToken);

        return entity;
    }

    public virtual async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        dbContext.Set<T>().AddRange(entities);

        await SaveChangesAsync(cancellationToken);

        return entities;
    }

    
    public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        dbContext.Set<T>().Update(entity);

        await SaveChangesAsync(cancellationToken);
    }

    
    public virtual async Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        dbContext.Set<T>().UpdateRange(entities);

        await SaveChangesAsync(cancellationToken);
    }

    public virtual async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        dbContext.Set<T>().Remove(entity);

        await SaveChangesAsync(cancellationToken);
    }

    public virtual async Task<bool> DeleteAsync(Expression<Func<T, bool>> expr, CancellationToken cancellationToken = default)
    {
        var result = false;

        var entity = dbContext.Set<T>().FirstOrDefault(expr);

        if (entity != null)
        {
            dbContext.Set<T>().Remove(entity);

            await SaveChangesAsync(cancellationToken);

            result = true;
        }

        return result;
    }

    public virtual async Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        dbContext.Set<T>().RemoveRange(entities);

        await SaveChangesAsync(cancellationToken);
    }


    //---

    public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.SaveChangesAsync(cancellationToken);
    }
}