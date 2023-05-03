using MediatR;

namespace Parameters.Application.Notification.Command.BankAccount;

public class CreateBankAccountNotificationCommand : INotification
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Balance { get; set; }
}