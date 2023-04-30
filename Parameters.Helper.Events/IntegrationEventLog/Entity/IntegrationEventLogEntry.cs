using System.Text.Json;
using System.Text.Json.Serialization;
using Parameters.Helper.Events.EventBus.Entity;
using Parameters.Helper.Events.IntegrationEventLog.Enums;

namespace Parameters.Helper.Events.IntegrationEventLog.Entity;

public class IntegrationEventLogEntry
{
    private IntegrationEventLogEntry()
    {
    }

    public IntegrationEventLogEntry(IntegrationEvent @event, Guid transactionId)
    {
        EventId = @event.Id;
        CreationDate = @event.CreationDate;
        EventTypeName = @event.GetType().FullName;
        Payload = JsonSerializer.Serialize(@event, @event.GetType(),
            new JsonSerializerOptions { WriteIndented = true });
        State = EventState.NotPublished;
        TimesSent = default;
        TransactionId = transactionId.ToString();
    }

    public Guid EventId { get; }

    public string? EventTypeName { get; }

    public string EventTypeShortName => EventTypeName?.Split('.')?.Last()!;

    public IntegrationEvent IntegrationEvent { get; private set; } = null!;

    public EventState State { get; set; }

    public int TimesSent { get; set; }

    public DateTime CreationDate { get; }

    public string Payload { get; }

    public string TransactionId { get; }

    public IntegrationEventLogEntry DeserializeJsonContent(Type type)
    {
        IntegrationEvent =
            (JsonSerializer.Deserialize(Payload, type,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                    })
                as IntegrationEvent)!;
        return this;
    }
}