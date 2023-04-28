using Parameters.Domain.Repository.PaymentType;

namespace Parameters.Infra.Repository.PaymentType;

public class UpdateRepository : UpdateBaseRepository<PaymentTypeEntity>, IUpdateRepository
{
    public UpdateRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}