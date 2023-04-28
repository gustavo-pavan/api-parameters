using Parameters.Application.Request.Command.FlowParameter;
using Parameters.Domain.Entity.Enums;
using Parameters.Domain.Repository.FlowParameter;

namespace Parameters.Application.Request.Handler.FlowParameter;

public class CreateRequestCommandHandler : IRequestHandler<CreateRequestCommand, FlowParameterEntity>
{
    private readonly IFlowParameterCreateRepository _flowParameterCreateRepository;
    private readonly ILogger<CreateRequestCommandHandler> _logger;

    public CreateRequestCommandHandler(IFlowParameterCreateRepository flowParameterCreateRepository,
        ILogger<CreateRequestCommandHandler> logger)
    {
        _flowParameterCreateRepository = flowParameterCreateRepository;
        _logger = logger;
    }

    public async Task<FlowParameterEntity> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to create new flow");
            var flowParameter = new FlowParameterEntity(request.Name,
                FlowEnumeration.FromValue<FlowType>(request.FlowType), request.Description);

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