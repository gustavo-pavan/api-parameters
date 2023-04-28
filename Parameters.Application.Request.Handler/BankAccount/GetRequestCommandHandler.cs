﻿using Parameters.Application.Request.Command.BankAccount;
using Parameters.Domain.Repository.BankAccount;

namespace Parameters.Application.Request.Handler.BankAccount;

public class GetRequestCommandHandler : IRequestHandler<GetRequestCommand, IEnumerable<BankAccountEntity>>
{
    private readonly IGetRepository _getRepository;
    private readonly ILogger<GetRequestCommandHandler> _logger;

    public GetRequestCommandHandler(IGetRepository getRepository,
        ILogger<GetRequestCommandHandler> logger)
    {
        _getRepository = getRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<BankAccountEntity>> Handle(GetRequestCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to get accounts");
            _logger.LogInformation("Execute transaction with database");
            var result = await _getRepository.Execute();

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