using Parameters.Domain.Repository.PaymentType;

namespace Parameters.Infra.Repository.PaymentType;

public class PaymentTypeDeleteRepository : DeleteBaseRepository<PaymentTypeEntity>, IPaymentTypeDeleteRepository
{
    public PaymentTypeDeleteRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}