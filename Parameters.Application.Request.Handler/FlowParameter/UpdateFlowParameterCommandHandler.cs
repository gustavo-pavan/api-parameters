using Parameters.Application.Notification.Command.FlowParameter;
using Parameters.Application.Request.Command.FlowParameter;
using Parameters.Domain.Entity.Enums;
using Parameters.Domain.Repository.FlowParameter;

namespace Parameters.Application.Request.Handler.FlowParameter;

public class
    UpdateFlowParameterCommandHandler : IRequestHandler<UpdateFlowParameterUpdateRequestCommand, FlowParameterEntity>
{
    private readonly IUpdateFlowParameterRepository _updateFlowParameterRepository;
    private readonly ILogger<UpdateFlowParameterCommandHandler> _logger;

    public UpdateFlowParameterCommandHandler(IUpdateFlowParameterRepository updateFlowParameterRepository,
        ILogger<UpdateFlowParameterCommandHandler> logger)
    {
        _updateFlowParameterRepository = updateFlowParameterRepository;
        _logger = logger;
    }

    public async Task<FlowParameterEntity> Handle(UpdateFlowParameterUpdateRequestCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to update flow");
            var flowParameter = new FlowParameterEntity(request.Id, request.Name,
                FlowEnumeration.FromValue<FlowType>(request.FlowType),
                request.Description);
            _logger.LogInformation("Execute transaction with database");
            await _updateFlowParameterRepository.Execute(flowParameter);

            _logger.LogInformation("Send new notification to update flow parameter");
            flowParameter?.AddDomainEvent(new UpdateFlowParameterNotificationCommand
                { Id = flowParameter.Id, Name = flowParameter.Name });

            _logger.LogInformation("Update flow with success");
            return flowParameter;
        }
        catch (Exception e)
        {
            _logger.LogError($"Error Update: {e.Message}");
            throw;
        }
    }
}