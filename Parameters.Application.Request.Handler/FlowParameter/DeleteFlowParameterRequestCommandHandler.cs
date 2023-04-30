using Parameters.Application.Notification.Command.FlowParameter;
using Parameters.Application.Request.Command.FlowParameter;
using Parameters.Domain.Repository.FlowParameter;

namespace Parameters.Application.Request.Handler.FlowParameter;

public class DeleteFlowParameterRequestCommandHandler : IRequestHandler<DeleteFlowParameterRequestCommand, bool>
{
    private readonly IDeleteFlowParameterRepository _deleteFlowParameterRepository;
    private readonly IGetByIdFlowParameterRepository _getByIdFlowParameterRepository;
    private readonly ILogger<DeleteFlowParameterRequestCommandHandler> _logger;

    public DeleteFlowParameterRequestCommandHandler(IDeleteFlowParameterRepository deleteFlowParameterRepository,
        ILogger<DeleteFlowParameterRequestCommandHandler> logger,
        IGetByIdFlowParameterRepository getByIdFlowParameterRepository)
    {
        _deleteFlowParameterRepository = deleteFlowParameterRepository;
        _logger = logger;
        _getByIdFlowParameterRepository = getByIdFlowParameterRepository;
    }

    public async Task<bool> Handle(DeleteFlowParameterRequestCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to delete flow");
            _logger.LogInformation("Execute transaction with database");
            var flowParameter = await _getByIdFlowParameterRepository.Execute(request.Id);

            if (flowParameter == null) throw new ArgumentException("Entity not exist!");

            await _deleteFlowParameterRepository.Execute(flowParameter);

            _logger.LogInformation("Send new notification to delete parameter");
            flowParameter?.AddDomainEvent(new DeleteFlowParameterNotificationCommand() { Id = flowParameter.Id });

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