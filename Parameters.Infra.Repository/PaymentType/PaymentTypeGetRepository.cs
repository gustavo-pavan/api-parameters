using Parameters.Domain.Repository.PaymentType;

namespace Parameters.Infra.Repository.PaymentType;

public class PaymentTypeGetRepository : GetBaseRepository<PaymentTypeEntity>, IPaymentTypeGetRepository
{
    public PaymentTypeGetRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}