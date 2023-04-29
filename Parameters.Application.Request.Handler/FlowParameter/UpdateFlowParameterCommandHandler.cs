using Parameters.Application.Request.Command.FlowParameter;
using Parameters.Domain.Entity.Enums;
using Parameters.Domain.Repository.FlowParameter;

namespace Parameters.Application.Request.Handler.FlowParameter;

public class
    UpdateFlowParameterCommandHandler : IRequestHandler<UpdateFlowParameterUpdateRequestCommand, FlowParameterEntity>
{
    private readonly IFlowParameterUpdateRepository _flowParameterUpdateRepository;
    private readonly ILogger<UpdateFlowParameterCommandHandler> _logger;

    public UpdateFlowParameterCommandHandler(IFlowParameterUpdateRepository flowParameterUpdateRepository,
        ILogger<UpdateFlowParameterCommandHandler> logger)
    {
        _flowParameterUpdateRepository = flowParameterUpdateRepository;
        _logger = logger;
    }

    public async Task<FlowParameterEntity> Handle(UpdateFlowParameterUpdateRequestCommand flowParameterUpdateRequest,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to update flow");
            var flowParameter = new FlowParameterEntity(flowParameterUpdateRequest.Id, flowParameterUpdateRequest.Name,
                FlowEnumeration.FromValue<FlowType>(flowParameterUpdateRequest.FlowType),
                flowParameterUpdateRequest.Description);
            _logger.LogInformation("Execute transaction with database");
            await _flowParameterUpdateRepository.Execute(flowParameter);

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