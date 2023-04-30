namespace Parameters.Application.Integration.Command.PaymentType;

public class CreatePaymentTypeIntegrationCommand : IntegrationEvent
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}