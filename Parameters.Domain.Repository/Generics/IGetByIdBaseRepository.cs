namespace Parameters.Domain.Repository.Generics;

public interface IGetByIdBaseRepository<TBaseEntity> : IDisposable where TBaseEntity : BaseEntity
{
    Task<TBaseEntity?> Execute(Guid id);
}