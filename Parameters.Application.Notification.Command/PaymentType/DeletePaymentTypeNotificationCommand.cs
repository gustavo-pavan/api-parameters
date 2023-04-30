using MediatR;

namespace Parameters.Application.Notification.Command.PaymentType;

public class DeletePaymentTypeNotificationCommand : INotification
{
    public Guid Id { get; set; }
}