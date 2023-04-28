using Parameters.Domain.Repository.PaymentType;

namespace Parameters.Infra.Repository.PaymentType;

public class PaymentTypeUpdateRepository : UpdateBaseRepository<PaymentTypeEntity>, IPaymentTypeUpdateRepository
{
    public PaymentTypeUpdateRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}