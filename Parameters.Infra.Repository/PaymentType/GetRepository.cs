using Parameters.Domain.Repository.PaymentType;

namespace Parameters.Infra.Repository.PaymentType;

public class GetRepository : GetBaseRepository<PaymentTypeEntity>, IGetRepository
{
    public GetRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}