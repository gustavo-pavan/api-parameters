using Parameters.Helper.Events.EventBus.Entity;

namespace Parameters.Helper.Events.EventBus.Interfaces;

public interface IEvent
{
    void Publish(IntegrationEvent @event);

    void Subscribe<T, U>()
        where T : IntegrationEvent
        where U : IIntegrationEventHandler<T>;

    void SubscribeDynamic<T>(string @event)
        where T : IIntegrationEventDynamicHandler;

    void UnsubscribeDynamic<T>(string @event)
        where T : IIntegrationEventDynamicHandler;

    void Unsubscribe<T, U>()
        where T : IIntegrationEventHandler<U>
        where U : IntegrationEvent;
}