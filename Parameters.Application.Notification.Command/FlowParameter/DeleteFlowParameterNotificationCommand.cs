using MediatR;

namespace Parameters.Application.Notification.Command.FlowParameter;

public class DeleteFlowParameterNotificationCommand : INotification
{
    public Guid Id { get; set; }
}