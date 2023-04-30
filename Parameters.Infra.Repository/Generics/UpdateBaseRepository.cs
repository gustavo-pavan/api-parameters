namespace Parameters.Infra.Repository.Generics;

public class UpdateBaseRepository<TBaseEntity> : IUpdateBaseRepository<TBaseEntity> where TBaseEntity : BaseEntity
{
    private readonly IMongoContext _mongoContext;

    public UpdateBaseRepository(IMongoContext mongoContext)
    {
        _mongoContext = mongoContext;
        Collection = mongoContext.GetCollection<TBaseEntity>(typeof(TBaseEntity).Name);
    }

    public IMongoCollection<TBaseEntity> Collection { get; }

    public Task Execute(TBaseEntity entity)
    {
        if (Guid.Empty.Equals(entity.Id))
            throw new ArgumentException($"Can't update because {nameof(BaseEntity.Id)} is not valid!");

        _mongoContext.AddCommand(entity, async () =>
            {
                var result = await Collection.ReplaceOneAsync(Builders<TBaseEntity>.Filter.Eq("_id", entity.Id), entity);
                return result.IsModifiedCountAvailable && result.ModifiedCount > 0;

            });
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _mongoContext?.Dispose();
    }
}