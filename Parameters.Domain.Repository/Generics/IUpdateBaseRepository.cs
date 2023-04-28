namespace Parameters.Domain.Repository.Generics;

public interface IUpdateBaseRepository<in TBaseEntity> : IDisposable where TBaseEntity : BaseEntity
{
    Task Execute(TBaseEntity entity);
}