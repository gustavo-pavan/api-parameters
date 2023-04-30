using Parameters.Application.Request.Command.FlowParameter;
using Parameters.Domain.Repository.FlowParameter;

namespace Parameters.Application.Request.Query.FlowParameter;

public class
    GetRequestFlowParameterCommandHandler : IRequestHandler<GetFlowParameterRequestCommand,
        IEnumerable<FlowParameterEntity>>
{
    private readonly IGetFlowParameterRepository _getFlowParameterRepository;
    private readonly ILogger<GetRequestFlowParameterCommandHandler> _logger;

    public GetRequestFlowParameterCommandHandler(IGetFlowParameterRepository getFlowParameterRepository,
        ILogger<GetRequestFlowParameterCommandHandler> logger)
    {
        _getFlowParameterRepository = getFlowParameterRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<FlowParameterEntity>> Handle(GetFlowParameterRequestCommand flowParameterRequest,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to get flow");
            _logger.LogInformation("Execute transaction with database");
            var result = await _getFlowParameterRepository.Execute();

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