using Parameters.Application.Request.Command.FlowParameter;
using Parameters.Domain.Repository.FlowParameter;

namespace Parameters.Application.Request.Query.FlowParameter;

public class
    GetByIdFlowParameterRequestCommandHandler : IRequestHandler<GetByIdFlowParameterRequestCommand, FlowParameterEntity
        ?>
{
    private readonly IFlowParameterGetByIdRepository _flowParameterGetByIdRepository;
    private readonly ILogger<GetByIdFlowParameterRequestCommandHandler> _logger;

    public GetByIdFlowParameterRequestCommandHandler(IFlowParameterGetByIdRepository flowParameterGetByIdRepository,
        ILogger<GetByIdFlowParameterRequestCommandHandler> logger)
    {
        _flowParameterGetByIdRepository = flowParameterGetByIdRepository;
        _logger = logger;
    }

    public async Task<FlowParameterEntity?> Handle(GetByIdFlowParameterRequestCommand flowParameterRequest,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to get flow");
            _logger.LogInformation("Execute transaction with database");
            var result = await _flowParameterGetByIdRepository.Execute(flowParameterRequest.Id);

            _logger.LogInformation("Get flow with success");
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError($"Error: {e.Message}");
            throw;
        }
    }
}