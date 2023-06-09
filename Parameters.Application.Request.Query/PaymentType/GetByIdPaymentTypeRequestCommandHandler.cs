﻿using Parameters.Application.Request.Command.PaymentType;
using Parameters.Domain.Repository.PaymentType;

namespace Parameters.Application.Request.Query.PaymentType;

public class
    GetByIdPaymentTypeRequestCommandHandler : IRequestHandler<GetByIdPaymentTypeRequestCommand, PaymentTypeEntity?>
{
    private readonly ILogger<GetByIdPaymentTypeRequestCommandHandler> _logger;
    private readonly IGetByIdPaymentTypeRepository _getByIdPaymentTypeRepository;

    public GetByIdPaymentTypeRequestCommandHandler(IGetByIdPaymentTypeRepository getByIdPaymentTypeRepository,
        ILogger<GetByIdPaymentTypeRequestCommandHandler> logger)
    {
        _getByIdPaymentTypeRepository = getByIdPaymentTypeRepository;
        _logger = logger;
    }

    public async Task<PaymentTypeEntity?> Handle(GetByIdPaymentTypeRequestCommand paymentTypeRequest,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to get payment type");
            _logger.LogInformation("Execute transaction with database");
            var result = await _getByIdPaymentTypeRepository.Execute(paymentTypeRequest.Id);

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