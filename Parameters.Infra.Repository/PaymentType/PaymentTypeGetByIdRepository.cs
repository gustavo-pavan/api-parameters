using Parameters.Domain.Repository.PaymentType;

namespace Parameters.Infra.Repository.PaymentType;

public class PaymentTypeGetByIdRepository : GetByIdBaseRepository<PaymentTypeEntity>, IPaymentTypeGetByIdRepository
{
    public PaymentTypeGetByIdRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}