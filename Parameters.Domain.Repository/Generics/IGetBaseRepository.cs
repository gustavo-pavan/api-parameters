namespace Parameters.Domain.Repository.Generics;

public interface IGetBaseRepository<TBaseEntity> : IDisposable where TBaseEntity : BaseEntity
{
    Task<IEnumerable<TBaseEntity>> Execute();
}