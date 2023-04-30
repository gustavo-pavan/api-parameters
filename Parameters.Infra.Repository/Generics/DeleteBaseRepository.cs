﻿namespace Parameters.Infra.Repository.Generics;

public class DeleteBaseRepository<TBaseEntity> : IDeleteBaseRepository<TBaseEntity> where TBaseEntity : BaseEntity
{
    private readonly IMongoContext _mongoContext;

    public DeleteBaseRepository(IMongoContext mongoContext)
    {
        _mongoContext = mongoContext;
        Collection = mongoContext.GetCollection<TBaseEntity>(typeof(TBaseEntity).Name);
    }

    public IMongoCollection<TBaseEntity> Collection { get; }

    public Task Execute(Guid id)
    {
        if (Guid.Empty.Equals(id))
            throw new OperationCanceledException($"Can't delete because {nameof(BaseEntity.Id)} is not valid!");

        _mongoContext.AddCommand<TBaseEntity>(default,
            () => Collection.DeleteOneAsync(Builders<TBaseEntity>.Filter.Eq("_id", id)));
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _mongoContext?.Dispose();
    }
}