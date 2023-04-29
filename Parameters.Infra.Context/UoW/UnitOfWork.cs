using Parameters.Helper.Events.IntegrationEventLog.Context;
using Parameters.Helper.Events.IntegrationEventLog.Services;

namespace Parameters.Infra.Context.UoW;

public class UnitOfWork : IDisposable, IUnitOfWork
{
    private readonly ILogger<UnitOfWork> _logger;
    private readonly IMongoContext _mongoContext;
    private IClientSessionHandle? _session;
    private readonly IntegrationEventContext _integrationEventContext;
    private readonly IParameterIntegrationEventService _parameterIntegrationEventService;


    public UnitOfWork(IMongoContext mongoContext, ILogger<UnitOfWork> logger, IntegrationEventContext integrationEventContext, IParameterIntegrationEventService parameterIntegrationEventService)
    {
        _mongoContext = mongoContext;
        _logger = logger;
        _integrationEventContext = integrationEventContext;
        _parameterIntegrationEventService = parameterIntegrationEventService;
    }

    private bool Disposed { get; set; }
    public bool HasActiveTransaction => _session != null;
   

    public void Dispose()
    {
        _logger.LogInformation("Dispose unit of work");
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async Task BeginTransactionAsync()
    {
        try
        {
            _logger.LogInformation("Start transaction");
            if (_session is not null) return;

            _session = await _mongoContext.SessionHandle;
            _session.StartTransaction();
            _logger.LogInformation("Finish transaction");
        }
        catch (Exception e)
        {
            _logger.LogError($"Error open transaction: {e.Message}");
            throw;
        }
    }


    public async Task RollbackAsync()
    {
        try
        {
            if (_session is not null)
            {
                _logger.LogInformation("Rollback transaction");
                await _session.AbortTransactionAsync();
            }
        }
        finally
        {
            if (_session is not null)
            {
                _session.Dispose();
                _session = null;
            }

            _logger.LogInformation("Finish rollback");
        }
    }

    public async Task CommitAsync()
    {
        try
        {
            _logger.LogInformation("Start commit");

            var count = await _mongoContext.SaveChanges();

            if (count > 0) await _integrationEventContext.SaveChangesAsync();

            _logger.LogInformation("Finish commit");
        }
        catch
        {
            _logger.LogInformation("Rollback transaction");
            await RollbackAsync();
        }
        finally
        {
            _logger.LogInformation("Dispose transaction");
            if (_session is not null)
            {
                await _parameterIntegrationEventService.PublishEventsThroughEventBusAsync(SingletonTransaction.TransactionId);
                _session?.Dispose();
                _mongoContext?.Dispose();
            }
        }
    }

    public virtual void Dispose(bool disposing)
    {
        _logger.LogInformation("Dispose Unit of Work virtual");
        if (!Disposed)
            if (disposing)
                _mongoContext.Dispose();

        Disposed = true;
    }
}