using Parameters.Application.Request.Command.FlowParameter;
using Parameters.Domain.Repository.FlowParameter;

namespace Parameters.Application.Request.Handler.FlowParameter;

public class GetRequestCommandHandler : IRequestHandler<GetRequestCommand, IEnumerable<FlowParameterEntity>>
{
    private readonly IGetRepository _getRepository;
    private readonly ILogger<GetRequestCommandHandler> _logger;

    public GetRequestCommandHandler(IGetRepository getRepository,
        ILogger<GetRequestCommandHandler> logger)
    {
        _getRepository = getRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<FlowParameterEntity>> Handle(GetRequestCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to get flow");
            _logger.LogInformation("Execute transaction with database");
            var result = await _getRepository.Execute();

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