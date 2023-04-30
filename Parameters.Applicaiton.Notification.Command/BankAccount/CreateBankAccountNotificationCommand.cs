using MediatR;

namespace Parameters.Applicaiton.Notification.Command.BankAccount;

public class CreateBankAccountNotificationCommand : INotification
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}