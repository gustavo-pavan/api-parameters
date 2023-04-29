using Parameters.Application.Request.Command.FlowParameter;
using Parameters.Domain.Repository.FlowParameter;

namespace Parameters.Application.Request.Handler.FlowParameter;

public class DeleteFlowParameterRequestCommandHandler : IRequestHandler<DeleteFlowParameterRequestCommand, bool>
{
    private readonly IFlowParameterDeleteRepository _flowParameterDeleteRepository;
    private readonly ILogger<DeleteFlowParameterRequestCommandHandler> _logger;

    public DeleteFlowParameterRequestCommandHandler(IFlowParameterDeleteRepository flowParameterDeleteRepository,
        ILogger<DeleteFlowParameterRequestCommandHandler> logger)
    {
        _flowParameterDeleteRepository = flowParameterDeleteRepository;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteFlowParameterRequestCommand flowParameterRequest,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to delete flow");
            _logger.LogInformation("Execute transaction with database");
            await _flowParameterDeleteRepository.Execute(flowParameterRequest.Id);

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