namespace MCafe.Shared.Interfaces;

public interface IReadOnlyRepository
{
    Task<T?> GetById<T>(Guid id, CancellationToken ct = default) where T : class, IGuidKeyedEntity;
    IQueryable<T> Items<T>() where T : class;
}

public interface IRepository : IReadOnlyRepository
{
    T Add<T>(T item) where T : class;
    void Remove<T>(T item) where T : class;
    Task SaveChanges(CancellationToken ct = default);
}
