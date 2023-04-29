using Parameters.Helper.Events.EventBus.Entity;

namespace Parameters.Helper.Events.EventBus.Interfaces;

public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
    where TIntegrationEvent : IntegrationEvent
{
    Task Handler(TIntegrationEvent @event);
}

public interface IIntegrationEventHandler
{
}