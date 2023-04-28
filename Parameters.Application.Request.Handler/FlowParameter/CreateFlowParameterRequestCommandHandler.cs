using Parameters.Application.Request.Command.FlowParameter;
using Parameters.Domain.Entity.Enums;
using Parameters.Domain.Repository.FlowParameter;

namespace Parameters.Application.Request.Handler.FlowParameter;

public class CreateFlowParameterRequestCommandHandler : IRequestHandler<CreateFlowParameterRequestCommand, FlowParameterEntity>
{
    private readonly IFlowParameterCreateRepository _flowParameterCreateRepository;
    private readonly ILogger<CreateFlowParameterRequestCommandHandler> _logger;

    public CreateFlowParameterRequestCommandHandler(IFlowParameterCreateRepository flowParameterCreateRepository,
        ILogger<CreateFlowParameterRequestCommandHandler> logger)
    {
        _flowParameterCreateRepository = flowParameterCreateRepository;
        _logger = logger;
    }

    public async Task<FlowParameterEntity> Handle(CreateFlowParameterRequestCommand flowParameterRequest, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to create new flow");
            var flowParameter = new FlowParameterEntity(flowParameterRequest.Name,
                FlowEnumeration.FromValue<FlowType>(flowParameterRequest.FlowType), flowParameterRequest.Description);

            _logger.LogInformation("Execute transaction with database");
            await _flowParameterCreateRepository.Execute(flowParameter);

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