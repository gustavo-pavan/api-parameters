namespace Parameters.Helper.Events.IntegrationEventLog.Services;

public interface IParameterIntegrationEventService
{
    Task PublishEventsThroughEventBusAsync(Guid transactionId);
}