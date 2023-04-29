namespace Parameters.Helper.Events.EventBus.Entity;

public class IntegrationEvent
{
    public IntegrationEvent(DateTime creationDate, Guid id)
    {
        CreationDate = creationDate;
        Id = id;
    }

    public IntegrationEvent()
    {
        Id = Guid.NewGuid();
        CreationDate = DateTime.Now;
    }

    public Guid Id { get; }
    public DateTime CreationDate { get; }
}