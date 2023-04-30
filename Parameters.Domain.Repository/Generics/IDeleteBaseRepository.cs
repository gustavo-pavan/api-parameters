namespace Parameters.Domain.Repository.Generics;

public interface IDeleteBaseRepository<in TBaseEntity> : IDisposable where TBaseEntity : BaseEntity
{
    Task Execute(TBaseEntity entity);
}