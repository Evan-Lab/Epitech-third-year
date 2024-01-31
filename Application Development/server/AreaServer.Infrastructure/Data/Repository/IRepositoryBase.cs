
using System.Linq.Expressions;

namespace AreaServer.Infrastructure.Data.Repository;

public interface IRepositoryBase<T> where T : class
{
    Task<T?> GetAsync(Expression<Func<T, bool>> expr, CancellationToken cancellationToken = default);

    //---

    Task<List<T>?> ListAsync(Expression<Func<T, bool>> expr, CancellationToken cancellationToken = default);
    Task<List<T>?> ListAsync(CancellationToken cancellationToken = default);

    //---
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);

    Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

    Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(Expression<Func<T, bool>> expr, CancellationToken cancellationToken = default);
    Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    

    //---

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);


}