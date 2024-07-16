using MCafe.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MCafe.Shared;

public class ReadOnlyRepository<TDbContext>(TDbContext dbContext) : IReadOnlyRepository where TDbContext : DbContext
{
    protected readonly TDbContext DbContext = dbContext;

    public Task<T?> GetById<T>(Guid id, CancellationToken ct) where T : class, IGuidKeyedEntity
        => Items<T>().SingleOrDefaultAsync(x => x.Id == id, ct);

    public virtual IQueryable<T> Items<T>() where T : class => DbContext.Set<T>().AsNoTracking();
}

public class Repository<TDbContext>(TDbContext dbContext)
    : ReadOnlyRepository<TDbContext>(dbContext), IRepository where TDbContext : DbContext
{
    public override IQueryable<T> Items<T>() where T : class => DbContext.Set<T>().AsQueryable();
    public T Add<T>(T item) where T : class => DbContext.Set<T>().Add(item).Entity;

    public void Remove<T>(T item) where T : class => DbContext.Set<T>().Remove(item);

    public Task SaveChanges(CancellationToken ct) => DbContext.SaveChangesAsync(ct);
}
