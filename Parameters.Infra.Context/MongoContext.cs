using MediatR;
using Parameters.Domain.Entity;

namespace Parameters.Infra.Context;

public class MongoContext : IMongoContext
{
    private readonly List<IDictionary<object, object>> _commands;

    private readonly IConfiguration _configuration;

    private readonly IMediator _mediator;

    private IMongoDatabase? _database;


    public MongoContext(IConfiguration configuration, IMediator mediator)
    {
        _configuration = configuration;
        _commands = new List<IDictionary<object, object>>();
        _mediator = mediator;
    }

    private Task<IClientSessionHandle>? Session { get; set; }

    public MongoClient? MongoClient { get; set; }

    public Task<IClientSessionHandle> SessionHandle
    {
        get
        {
            if (Session != null) return Session;

            ConfigureMongo();

            if (MongoClient != null)
                Session = MongoClient.StartSessionAsync();

            return Session ?? throw new InvalidOperationException("Session can't be create");
        }
    }

    public void Dispose()
    {
        SessionHandle?.Dispose();
        GC.SuppressFinalize(this);
    }

    public void AddCommand<TEntity>(TEntity entity, Func<Task<bool>> func) where TEntity : BaseEntity
    {
        _commands.Add(new Dictionary<object, object>
        {
            { entity, func }
        });
    }

    public async Task<int> SaveChanges(CancellationToken cancellation = new())
    {
        var commandTask = _commands.Select(x => x);

        foreach (var dict in commandTask)
        {
            foreach (var item in dict)
            {
                if (item.Value is not Func<Task<bool>> func) continue;

                var result = Task.Run(func, cancellation).Result;

                if (result)
                    await PublishEvent(cancellation, item.Key);
            }
        }

        return _commands.Count();
    }

    private async Task PublishEvent(CancellationToken cancellation, object item)
    {
        var baseEntity = item as BaseEntity;

        var domainEvents = baseEntity!.DomainEvents?.ToList();

        if (domainEvents == null) return;

        baseEntity?.ClearDomainEvents();

        foreach (var domainEvent in domainEvents)
            await _mediator.Publish(domainEvent, cancellation);
    }

    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        ConfigureMongo();
        return _database?.GetCollection<T>(collectionName) ??
               throw new InvalidOperationException("Database is not configured");
    }

    private void ConfigureMongo()
    {
        if (MongoClient != null) return;

        MongoClient = new MongoClient(_configuration["MongoSettings_Connection"]);
        _database = MongoClient.GetDatabase(_configuration["MongoSettings_DatabaseName"]);
    }
}