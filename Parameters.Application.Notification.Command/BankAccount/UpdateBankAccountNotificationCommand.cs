using MediatR;

namespace Parameters.Application.Notification.Command.BankAccount;

public class UpdateBankAccountNotificationCommand : INotification
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Balance { get; set; }
}