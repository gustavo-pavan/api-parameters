using Parameters.Helper.Events.EventBus.Entity;

namespace Parameters.Helper.Events.EventBus.Interfaces;

public interface IEventBusManager
{
    bool IsEmpty { get; }

    event EventHandler<string> OnEventRemoved;

    void AddSubscription<T>(string eventName)
        where T : IIntegrationEventDynamicHandler;

    void AddSubscription<T, U>()
        where T : IntegrationEvent
        where U : IIntegrationEventHandler<T>;

    void RemoveSubscription<T, U>()
        where U : IIntegrationEventHandler<T>
        where T : IntegrationEvent;

    void RemoveDynamicSubscription<T>(string eventName)
        where T : IIntegrationEventDynamicHandler;

    bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent;

    bool HasSubscriptionsForEvent(string eventName);

    Type GetEventTypeByName(string eventName);

    void Clear();

    IEnumerable<EventBusManager.SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent;

    IEnumerable<EventBusManager.SubscriptionInfo> GetHandlersForEvent(string eventName);

    string GetEventKey<T>();
}