using Parameters.Domain.Repository.PaymentType;

namespace Parameters.Infra.Repository.PaymentType;

public class PaymentTypeCreateRepository : CreateBaseRepository<PaymentTypeEntity>, IPaymentTypeCreateRepository
{
    public PaymentTypeCreateRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}