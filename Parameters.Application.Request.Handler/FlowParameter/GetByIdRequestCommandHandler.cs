﻿using Parameters.Application.Request.Command.FlowParameter;
using Parameters.Domain.Repository.FlowParameter;

namespace Parameters.Application.Request.Handler.FlowParameter;

public class GetByIdRequestCommandHandler : IRequestHandler<GetByIdRequestCommand, FlowParameterEntity?>
{
    private readonly IGetByIdRepository _getByIdRepository;
    private readonly ILogger<GetByIdRequestCommandHandler> _logger;

    public GetByIdRequestCommandHandler(IGetByIdRepository getByIdRepository,
        ILogger<GetByIdRequestCommandHandler> logger)
    {
        _getByIdRepository = getByIdRepository;
        _logger = logger;
    }

    public async Task<FlowParameterEntity?> Handle(GetByIdRequestCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to get flow");
            _logger.LogInformation("Execute transaction with database");
            var result = await _getByIdRepository.Execute(request.Id);

            _logger.LogInformation("Get flow with success");
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError($"Error: {e.Message}");
            throw;
        }
    }
}