using Parameters.Domain.Repository.PaymentType;

namespace Parameters.Infra.Repository.PaymentType;

public class GetByIdRepository : GetByIdBaseRepository<PaymentTypeEntity>, IGetByIdRepository
{
    public GetByIdRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}