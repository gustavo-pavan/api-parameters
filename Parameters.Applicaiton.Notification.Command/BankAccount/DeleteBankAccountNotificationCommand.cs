using MediatR;

namespace Parameters.Applicaiton.Notification.Command.BankAccount;

public class DeleteBankAccountNotificationCommand : INotification
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}