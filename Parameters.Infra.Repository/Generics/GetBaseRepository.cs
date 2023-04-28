namespace Parameters.Infra.Repository.Generics;

public class GetBaseRepository<TBaseEntity> : IGetBaseRepository<TBaseEntity> where TBaseEntity : BaseEntity
{
    private readonly IMongoContext _mongoContext;

    public GetBaseRepository(IMongoContext mongoContext)
    {
        _mongoContext = mongoContext;
        Collection = mongoContext.GetCollection<TBaseEntity>(typeof(TBaseEntity).Name);
    }

    public IMongoCollection<TBaseEntity> Collection { get; }


    public async Task<IEnumerable<TBaseEntity>> Execute()
    {
        var result = await Collection.FindAsync(Builders<TBaseEntity>.Filter.Empty);
        return await result.ToListAsync();
    }

    public void Dispose()
    {
        _mongoContext?.Dispose();
    }
}