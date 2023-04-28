using Parameters.Domain.Repository.FlowParameter;

namespace Parameters.Infra.Repository.FlowParameter;

public class FlowParameterCreateRepository : CreateBaseRepository<FlowParameterEntity>, IFlowParameterCreateRepository
{
    public FlowParameterCreateRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}