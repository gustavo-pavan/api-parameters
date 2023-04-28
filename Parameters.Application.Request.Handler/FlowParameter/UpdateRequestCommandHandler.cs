using Parameters.Application.Request.Command.FlowParameter;
using Parameters.Domain.Entity.Enums;
using Parameters.Domain.Repository.FlowParameter;

namespace Parameters.Application.Request.Handler.FlowParameter;

public class UpdateRequestCommandHandler : IRequestHandler<UpdateRequestCommand, FlowParameterEntity>
{
    private readonly ILogger<BankAccount.CreateRequestCommandHandler> _logger;
    private readonly IUpdateRepository _updateRepository;

    public UpdateRequestCommandHandler(IUpdateRepository updateRepository,
        ILogger<BankAccount.CreateRequestCommandHandler> logger)
    {
        _updateRepository = updateRepository;
        _logger = logger;
    }

    public async Task<FlowParameterEntity> Handle(UpdateRequestCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to update flow");
            var flowParameter = new FlowParameterEntity(request.Id, request.Name, FlowEnumeration.FromValue<FlowType>(request.FlowType), request.Description);
            _logger.LogInformation("Execute transaction with database");
            await _updateRepository.Execute(flowParameter);

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