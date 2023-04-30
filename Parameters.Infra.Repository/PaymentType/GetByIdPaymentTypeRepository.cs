using Parameters.Domain.Repository.PaymentType;

namespace Parameters.Infra.Repository.PaymentType;

public class GetByIdPaymentTypeRepository : GetByIdBaseRepository<PaymentTypeEntity>, IGetByIdPaymentTypeRepository
{
    public GetByIdPaymentTypeRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}