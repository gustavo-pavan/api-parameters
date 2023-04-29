using RabbitMQ.Client;

namespace Parameters.Helper.Events.EventRabbitMQ.Connection;

public interface IRabbitMQConnection : IDisposable
{
    bool IsConnected { get; }
    bool TryConnect();
    IModel CreateModel();
}