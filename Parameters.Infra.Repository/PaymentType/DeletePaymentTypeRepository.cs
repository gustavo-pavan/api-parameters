using Parameters.Domain.Repository.PaymentType;

namespace Parameters.Infra.Repository.PaymentType;

public class DeletePaymentTypeRepository : DeleteBaseRepository<PaymentTypeEntity>, IDeletePaymentTypeRepository
{
    public DeletePaymentTypeRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}