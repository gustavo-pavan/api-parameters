using Parameters.Domain.Repository.FlowParameter;

namespace Parameters.Infra.Repository.FlowParameter;

public class GetByIdFlowParameterRepository : GetByIdBaseRepository<FlowParameterEntity>,
    IGetByIdFlowParameterRepository
{
    public GetByIdFlowParameterRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}