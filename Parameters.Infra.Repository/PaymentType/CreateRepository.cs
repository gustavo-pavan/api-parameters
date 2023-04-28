using Parameters.Domain.Repository.PaymentType;

namespace Parameters.Infra.Repository.PaymentType;

public class CreateRepository : CreateBaseRepository<PaymentTypeEntity>, ICreateRepository
{
    public CreateRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}