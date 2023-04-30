using Parameters.Domain.Repository.PaymentType;

namespace Parameters.Infra.Repository.PaymentType;

public class UpdatePaymentTypeRepository : UpdateBaseRepository<PaymentTypeEntity>, IUpdatePaymentTypeRepository
{
    public UpdatePaymentTypeRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}