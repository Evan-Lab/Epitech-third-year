namespace AreaServer.Infrastructure.Data.Repository;


public class EfRepository<T> : RepositoryBase<T>, IRepository<T> where T : class
{
    public EfRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}

