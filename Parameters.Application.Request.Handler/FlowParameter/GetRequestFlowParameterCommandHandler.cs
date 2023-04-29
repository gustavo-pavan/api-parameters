using Parameters.Application.Request.Command.FlowParameter;
using Parameters.Domain.Repository.FlowParameter;

namespace Parameters.Application.Request.Handler.FlowParameter;

public class
    GetRequestFlowParameterCommandHandler : IRequestHandler<GetFlowParameterRequestCommand,
        IEnumerable<FlowParameterEntity>>
{
    private readonly IFlowParameterGetRepository _flowParameterGetRepository;
    private readonly ILogger<GetRequestFlowParameterCommandHandler> _logger;

    public GetRequestFlowParameterCommandHandler(IFlowParameterGetRepository flowParameterGetRepository,
        ILogger<GetRequestFlowParameterCommandHandler> logger)
    {
        _flowParameterGetRepository = flowParameterGetRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<FlowParameterEntity>> Handle(GetFlowParameterRequestCommand flowParameterRequest,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to get flow");
            _logger.LogInformation("Execute transaction with database");
            var result = await _flowParameterGetRepository.Execute();

            _logger.LogInformation("Get flows with success");
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError($"Error: {e.Message}");
            throw;
        }
    }
}