using Parameters.Domain.Repository.FlowParameter;

namespace Parameters.Infra.Repository.FlowParameter;

public class CreateFlowParameterRepository : CreateBaseRepository<FlowParameterEntity>, ICreateFlowParameterRepository
{
    public CreateFlowParameterRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}