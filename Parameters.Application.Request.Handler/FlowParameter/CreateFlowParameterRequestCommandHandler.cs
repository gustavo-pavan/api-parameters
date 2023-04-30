using Parameters.Application.Notification.Command.FlowParameter;
using Parameters.Application.Request.Command.FlowParameter;
using Parameters.Domain.Entity.Enums;
using Parameters.Domain.Repository.FlowParameter;

namespace Parameters.Application.Request.Handler.FlowParameter;

public class
    CreateFlowParameterRequestCommandHandler : IRequestHandler<CreateFlowParameterRequestCommand, FlowParameterEntity>
{
    private readonly ICreateFlowParameterRepository _createFlowParameterRepository;
    private readonly ILogger<CreateFlowParameterRequestCommandHandler> _logger;

    public CreateFlowParameterRequestCommandHandler(ICreateFlowParameterRepository createFlowParameterRepository,
        ILogger<CreateFlowParameterRequestCommandHandler> logger)
    {
        _createFlowParameterRepository = createFlowParameterRepository;
        _logger = logger;
    }

    public async Task<FlowParameterEntity> Handle(CreateFlowParameterRequestCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to create new flow");
            var flowParameter = new FlowParameterEntity(request.Name,
                FlowEnumeration.FromValue<FlowType>(request.FlowType), request.Description);

            _logger.LogInformation("Execute transaction with database");
            await _createFlowParameterRepository.Execute(flowParameter);

            _logger.LogInformation("Send new notification to create flow parameter");
            flowParameter.AddDomainEvent(new CreateFlowParameterNotificationCommand
                { Id = flowParameter.Id, Name = flowParameter.Name });

            _logger.LogInformation("Create flow with success");
            return flowParameter;
        }
        catch (Exception e)
        {
            _logger.LogError($"Error: {e.Message}");
            throw;
        }
    }
}