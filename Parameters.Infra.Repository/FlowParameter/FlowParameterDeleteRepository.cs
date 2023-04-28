using Parameters.Domain.Repository.FlowParameter;

namespace Parameters.Infra.Repository.FlowParameter;

public class FlowParameterDeleteRepository : DeleteBaseRepository<FlowParameterEntity>, IFlowParameterDeleteRepository
{
    public FlowParameterDeleteRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}