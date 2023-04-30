using MediatR;

namespace Parameters.Application.Notification.Command.PaymentType;

public class CreatePaymentTypeNotificationCommand : INotification
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}