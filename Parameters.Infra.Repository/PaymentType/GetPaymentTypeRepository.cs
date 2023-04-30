using Parameters.Domain.Repository.PaymentType;

namespace Parameters.Infra.Repository.PaymentType;

public class GetPaymentTypeRepository : GetBaseRepository<PaymentTypeEntity>, IGetPaymentTypeRepository
{
    public GetPaymentTypeRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}