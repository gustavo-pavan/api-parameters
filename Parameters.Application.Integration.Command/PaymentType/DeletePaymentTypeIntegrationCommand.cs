namespace Parameters.Application.Integration.Command.PaymentType;

public class DeletePaymentTypeIntegrationCommand : IntegrationEvent
{
    public Guid Id { get; set; }
}