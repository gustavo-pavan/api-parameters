using MediatR;

namespace Parameters.Applicaiton.Notification.Command.FlowParameter;

public class CreateFlowParameterNotificationCommand : INotification
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}