using Parameters.Helper.Events.EventBus.Entity;
using Parameters.Helper.Events.EventBus.Interfaces;

namespace Parameters.Helper.Events.EventBus;

public partial class EventBusManager : IEventBusManager
{
    private readonly List<Type> _eventTypes = new();

    private readonly IDictionary<string, List<SubscriptionInfo>>
        _handlers = new Dictionary<string, List<SubscriptionInfo>>();

    public event EventHandler<string>? OnEventRemoved;

    public bool IsEmpty => _handlers is { Count: 0 };

    public void Clear()
    {
        _handlers.Clear();
    }

    public void AddSubscription<T>(string eventName) where T : IIntegrationEventDynamicHandler
    {
        DoAddSubscription(typeof(T), eventName, true);
    }

    public void AddSubscription<T, U>() where T : IntegrationEvent where U : IIntegrationEventHandler<T>
    {
        var name = GetEventKey<T>();

        DoAddSubscription(typeof(U), name, false);

        if (_eventTypes.Contains(typeof(T)))
            _eventTypes.Add(typeof(T));
    }

    public void RemoveSubscription<T, U>() where T : IntegrationEvent where U : IIntegrationEventHandler<T>
    {
        var handlerToRemove = FindSubscriptionToRemove<T, U>();
        var eventName = GetEventKey<T>();
        DoRemoveHandler(eventName, handlerToRemove);
    }

    public void RemoveDynamicSubscription<T>(string eventName) where T : IIntegrationEventDynamicHandler
    {
        var handlerToRemove = FindDynamicSubscriptionToRemove<T>(eventName);
        DoRemoveHandler(eventName, handlerToRemove);
    }

    public bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent
    {
        var key = GetEventKey<T>();
        return HasSubscriptionsForEvent(key);
    }

    public bool HasSubscriptionsForEvent(string eventName)
    {
        return _handlers.ContainsKey(eventName);
    }

    public Type GetEventTypeByName(string eventName)
    {
        return _eventTypes.SingleOrDefault(t => t.Name == eventName)!;
    }

    public IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>()
        where T : IntegrationEvent
    {
        var key = GetEventKey<T>();
        return GetHandlersForEvent(key);
    }

    public IEnumerable<SubscriptionInfo> GetHandlersForEvent(
        string eventName)
    {
        return _handlers[eventName];
    }

    public string GetEventKey<T>()
    {
        return typeof(T).Name;
    }

    private void DoAddSubscription(Type type, string eventName, bool isDynamic)
    {
        if (!HasSubscriptionsForEvent(eventName))
            _handlers.Add(eventName, new List<SubscriptionInfo>());

        if (_handlers[eventName].Any(e => e.HandlerType == type))
            throw new ArgumentException($"Handler Type {type.Name} already registered for '{eventName}'", nameof(type));

        if (isDynamic)
            _handlers[eventName].Add(SubscriptionInfo.Dynamic(type));
        else
            _handlers[eventName].Add(SubscriptionInfo.Typed(type));
    }

    private void DoRemoveHandler(string eventName,
        SubscriptionInfo? handlerToRemove)
    {
        if (handlerToRemove == null) return;

        _handlers[eventName].Remove(handlerToRemove);

        if (_handlers[eventName].Any()) return;

        _handlers.Remove(eventName);

        var eventType = _eventTypes.SingleOrDefault(e => e.Name == eventName);

        if (eventType != null)
            _eventTypes.Remove(eventType);

        RaiseOnEventRemoved(eventName);
    }

    private void RaiseOnEventRemoved(string eventName)
    {
        var handler = OnEventRemoved;
        handler?.Invoke(this, eventName);
    }

    private SubscriptionInfo FindSubscriptionToRemove<T, U>()
        where T : IntegrationEvent
        where U : IIntegrationEventHandler<T>
    {
        var eventName = GetEventKey<T>();
        return DoFindSubscriptionToRemove(eventName, typeof(U));
    }


    private SubscriptionInfo DoFindSubscriptionToRemove(string eventName,
        Type type)
    {
        if (!HasSubscriptionsForEvent(eventName))
            return null!;

        return _handlers[eventName].SingleOrDefault(e => e.HandlerType == type)!;
    }

    private SubscriptionInfo FindDynamicSubscriptionToRemove<T>(
        string eventName)
        where T : IIntegrationEventDynamicHandler
    {
        return DoFindSubscriptionToRemove(eventName, typeof(T));
    }
}