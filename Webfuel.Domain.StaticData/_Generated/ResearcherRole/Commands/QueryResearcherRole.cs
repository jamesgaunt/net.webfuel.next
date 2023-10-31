using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QueryResearcherRole: Query, IRequest<QueryResult<ResearcherRole>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(ResearcherRole.Name), Search);
            return this;
        }
    }
    internal class QueryResearcherRoleHandler : IRequestHandler<QueryResearcherRole, QueryResult<ResearcherRole>>
    {
        private readonly IResearcherRoleRepository _researcherRoleRepository;
        
        
        public QueryResearcherRoleHandler(IResearcherRoleRepository researcherRoleRepository)
        {
            _researcherRoleRepository = researcherRoleRepository;
        }
        
        public async Task<QueryResult<ResearcherRole>> Handle(QueryResearcherRole request, CancellationToken cancellationToken)
        {
            return await _researcherRoleRepository.QueryResearcherRole(request.ApplyCustomFilters());
        }
    }
}

