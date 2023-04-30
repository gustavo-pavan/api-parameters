using Parameters.Domain.Repository.FlowParameter;

namespace Parameters.Infra.Repository.FlowParameter;

public class GetFlowParameterRepository : GetBaseRepository<FlowParameterEntity>, IGetFlowParameterRepository
{
    public GetFlowParameterRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}