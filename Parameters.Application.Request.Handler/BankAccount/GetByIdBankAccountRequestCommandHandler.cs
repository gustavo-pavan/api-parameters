﻿using Parameters.Application.Request.Command.BankAccount;
using Parameters.Domain.Repository.BankAccount;

namespace Parameters.Application.Request.Handler.BankAccount;

public class
    GetByIdBankAccountRequestCommandHandler : IRequestHandler<GetByIdBankAccountRequestCommand, BankAccountEntity?>
{
    private readonly IBaseAccountGetByIdRepository _baseAccountGetByIdRepository;
    private readonly ILogger<GetByIdBankAccountRequestCommandHandler> _logger;

    public GetByIdBankAccountRequestCommandHandler(IBaseAccountGetByIdRepository baseAccountGetByIdRepository,
        ILogger<GetByIdBankAccountRequestCommandHandler> logger)
    {
        _baseAccountGetByIdRepository = baseAccountGetByIdRepository;
        _logger = logger;
    }

    public async Task<BankAccountEntity?> Handle(GetByIdBankAccountRequestCommand bankAccountRequest,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to get accounts");
            _logger.LogInformation("Execute transaction with database");
            var result = await _baseAccountGetByIdRepository.Execute(bankAccountRequest.Id);

            _logger.LogInformation("Get accounts with success");
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError($"Error: {e.Message}");
            throw;
        }
    }
}