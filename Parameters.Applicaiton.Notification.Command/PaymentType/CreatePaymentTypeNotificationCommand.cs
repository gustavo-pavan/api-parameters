using MediatR;

namespace Parameters.Applicaiton.Notification.Command.PaymentType;

public class CreatePaymentTypeNotificationCommand : INotification
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}