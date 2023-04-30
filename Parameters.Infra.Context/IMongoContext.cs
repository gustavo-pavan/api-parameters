using Parameters.Domain.Entity;

namespace Parameters.Infra.Context;

public interface IMongoContext : IDisposable
{
    public Task<IClientSessionHandle> SessionHandle { get; }
    void AddCommand<TEntity>(TEntity entity, Func<Task> func) where TEntity : BaseEntity;
    Task<int> SaveChanges(CancellationToken cancellation = new());
    IMongoCollection<T> GetCollection<T>(string collectionName);
}