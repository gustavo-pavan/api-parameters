using MediatR;

namespace Parameters.Applicaiton.Notification.Command.PaymentType;

public class UpdatePaymentTypeNotificationCommand : INotification
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}