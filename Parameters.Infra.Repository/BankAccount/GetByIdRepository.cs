using Parameters.Domain.Repository.BankAccount;

namespace Parameters.Infra.Repository.BankAccount;

public class GetByIdRepository: GetByIdBaseRepository<BankAccountEntity>, IGetByIdRepository
{
    public GetByIdRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}