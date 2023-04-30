using MediatR;
using MongoDB.Bson.Serialization.Attributes;

namespace Parameters.Domain.Entity;

public abstract class BaseEntity
{
    public Guid Id { get; protected set; }

    private List<INotification>? _domainEvents;

    [BsonIgnore]
    public IReadOnlyCollection<INotification>? DomainEvents => _domainEvents?.AsReadOnly();

    public void AddDomainEvent(INotification eventItem)
    {
        _domainEvents ??= new List<INotification>();
        _domainEvents.Add(eventItem);
    }

    public void RemoveDomainEvent(INotification eventItem)
    {
        _domainEvents?.Remove(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }
}