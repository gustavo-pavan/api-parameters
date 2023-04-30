using Parameters.Domain.Repository.PaymentType;

namespace Parameters.Infra.Repository.PaymentType;

public class CreatePaymentTypeRepository : CreateBaseRepository<PaymentTypeEntity>, ICreatePaymentTypeRepository
{
    public CreatePaymentTypeRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}