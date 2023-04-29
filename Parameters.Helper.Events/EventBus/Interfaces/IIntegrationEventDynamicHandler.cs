namespace Parameters.Helper.Events.EventBus.Interfaces;

public interface IIntegrationEventDynamicHandler
{
    Task Handler(dynamic @event);
}