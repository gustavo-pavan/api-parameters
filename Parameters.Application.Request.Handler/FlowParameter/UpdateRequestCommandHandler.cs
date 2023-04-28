using Parameters.Application.Request.Command.FlowParameter;
using Parameters.Domain.Entity.Enums;
using Parameters.Domain.Repository.FlowParameter;

namespace Parameters.Application.Request.Handler.FlowParameter;

public class UpdateRequestCommandHandler : IRequestHandler<UpdateRequestCommand, FlowParameterEntity>
{
    private readonly IFlowParameterUpdateRepository _flowParameterUpdateRepository;
    private readonly ILogger<UpdateRequestCommandHandler> _logger;

    public UpdateRequestCommandHandler(IFlowParameterUpdateRepository flowParameterUpdateRepository,
        ILogger<UpdateRequestCommandHandler> logger)
    {
        _flowParameterUpdateRepository = flowParameterUpdateRepository;
        _logger = logger;
    }

    public async Task<FlowParameterEntity> Handle(UpdateRequestCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to update flow");
            var flowParameter = new FlowParameterEntity(request.Id, request.Name,
                FlowEnumeration.FromValue<FlowType>(request.FlowType), request.Description);
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