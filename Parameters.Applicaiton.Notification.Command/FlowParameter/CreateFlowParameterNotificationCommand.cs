using MediatR;

namespace Parameters.Application.Notification.Command.BankAccount;

public class CreateFlowParameterNotificationCommand : INotification
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
