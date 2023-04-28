using Parameters.Application.Request.Command.PaymentType;
using Parameters.Domain.Repository.PaymentType;

namespace Parameters.Application.Request.Handler.PaymentType;

public class GetByIdRequestCommandHandler : IRequestHandler<GetByIdRequestCommand, PaymentTypeEntity?>
{
    private readonly IGetByIdRepository _getByIdRepository;
    private readonly ILogger<GetByIdRequestCommandHandler> _logger;

    public GetByIdRequestCommandHandler(IGetByIdRepository getByIdRepository,
        ILogger<GetByIdRequestCommandHandler> logger)
    {
        _getByIdRepository = getByIdRepository;
        _logger = logger;
    }

    public async Task<PaymentTypeEntity?> Handle(GetByIdRequestCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to get payment type");
            _logger.LogInformation("Execute transaction with database");
            var result = await _getByIdRepository.Execute(request.Id);

            _logger.LogInformation("Get payment type with success");
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError($"Error: {e.Message}");
            throw;
        }
    }
}