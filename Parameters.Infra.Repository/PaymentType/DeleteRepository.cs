using Parameters.Domain.Repository.PaymentType;

namespace Parameters.Infra.Repository.PaymentType;

public class DeleteRepository : DeleteBaseRepository<PaymentTypeEntity>, IDeleteRepository
{
    public DeleteRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}