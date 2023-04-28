namespace Parameters.Domain.Repository.Generics;

public interface ICreateBaseRepository<in TBaseEntity> : IDisposable where TBaseEntity : BaseEntity
{
    Task Execute(TBaseEntity entity);
}