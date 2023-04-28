using Parameters.Application.Request.Command.FlowParameter;
using Parameters.Domain.Repository.FlowParameter;

namespace Parameters.Application.Request.Handler.FlowParameter;

public class DeleteRequestCommandHandler : IRequestHandler<DeleteRequestCommand, bool>
{
    private readonly IFlowParameterDeleteRepository _flowParameterDeleteRepository;
    private readonly ILogger<DeleteRequestCommandHandler> _logger;

    public DeleteRequestCommandHandler(IFlowParameterDeleteRepository flowParameterDeleteRepository,
        ILogger<DeleteRequestCommandHandler> logger)
    {
        _flowParameterDeleteRepository = flowParameterDeleteRepository;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteRequestCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to delete flow");
            _logger.LogInformation("Execute transaction with database");
            await _flowParameterDeleteRepository.Execute(request.Id);

            _logger.LogInformation("Delete flow with success");
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError($"Error: {e.Message}");
            throw;
        }
    }
}