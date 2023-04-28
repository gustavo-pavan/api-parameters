namespace Parameters.Infra.Context.UoW;

public interface IUnitOfWork
{
    Task CommitAsync();
    Task BeginTransactionAsync();
    Task RollbackAsync();
}