namespace Parameters.Application.Integration.Command.PaymentType;

public class UpdatePaymentTypeIntegrationCommand : IntegrationEvent
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}