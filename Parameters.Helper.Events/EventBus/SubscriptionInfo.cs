using Parameters.Helper.Events.EventBus.Interfaces;

namespace Parameters.Helper.Events.EventBus;

public partial class EventBusManager : IEventBusManager
{
    public class SubscriptionInfo
    {
        private SubscriptionInfo(bool isDynamic, Type handlerType)
        {
            IsDynamic = isDynamic;
            HandlerType = handlerType;
        }

        public bool IsDynamic { get; }
        public Type HandlerType { get; }

        public static SubscriptionInfo Dynamic(Type handlerType)
        {
            return new SubscriptionInfo(true, handlerType);
        }

        public static SubscriptionInfo Typed(Type handlerType)
        {
            return new SubscriptionInfo(false, handlerType);
        }
    }
}