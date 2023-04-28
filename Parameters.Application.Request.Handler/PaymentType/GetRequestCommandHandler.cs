using Parameters.Application.Request.Command.PaymentType;
using Parameters.Domain.Repository.PaymentType;

namespace Parameters.Application.Request.Handler.PaymentType;

public class GetRequestCommandHandler : IRequestHandler<GetRequestCommand, IEnumerable<PaymentTypeEntity>>
{
    private readonly IGetRepository _getRepository;
    private readonly ILogger<GetRequestCommandHandler> _logger;

    public GetRequestCommandHandler(IGetRepository getRepository,
        ILogger<GetRequestCommandHandler> logger)
    {
        _getRepository = getRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<PaymentTypeEntity>> Handle(GetRequestCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to get payment type");
            _logger.LogInformation("Execute transaction with database");
            var result = await _getRepository.Execute();

            _logger.LogInformation("Get payments type lows with success");
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError($"Error: {e.Message}");
            throw;
        }
    }
}