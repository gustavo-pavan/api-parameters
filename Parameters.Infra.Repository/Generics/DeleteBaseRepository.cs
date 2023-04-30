namespace Parameters.Infra.Repository.Generics;

public class DeleteBaseRepository<TBaseEntity> : IDeleteBaseRepository<TBaseEntity> where TBaseEntity : BaseEntity
{
    private readonly IMongoContext _mongoContext;

    public DeleteBaseRepository(IMongoContext mongoContext)
    {
        _mongoContext = mongoContext;
        Collection = mongoContext.GetCollection<TBaseEntity>(typeof(TBaseEntity).Name);
    }

    public IMongoCollection<TBaseEntity> Collection { get; }

    public Task Execute(TBaseEntity entity)
    {
        if (Guid.Empty.Equals(entity.Id))
            throw new OperationCanceledException($"Can't delete because {nameof(BaseEntity.Id)} is not valid!");

        _mongoContext.AddCommand<TBaseEntity>(entity, async () =>
            {
                var result = await Collection.DeleteOneAsync(Builders<TBaseEntity>.Filter.Eq("_id", entity.Id));
                return result.DeletedCount > 0;
            });

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _mongoContext?.Dispose();
    }
}