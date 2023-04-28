using Parameters.Domain.Repository.FlowParameter;

namespace Parameters.Infra.Repository.FlowParameter;

public class GetByIdRepository : GetByIdBaseRepository<FlowParameterEntity>, IGetByIdRepository
{
    public GetByIdRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}