using Parameters.Domain.Repository.FlowParameter;

namespace Parameters.Infra.Repository.FlowParameter;

public class CreateRepository : CreateBaseRepository<FlowParameterEntity>, ICreateRepository
{
    public CreateRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}