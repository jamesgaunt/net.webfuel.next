using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QueryResearcherOrganisationType: Query, IRequest<QueryResult<ResearcherOrganisationType>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(ResearcherOrganisationType.Name), Search);
            return this;
        }
    }
    internal class QueryResearcherOrganisationTypeHandler : IRequestHandler<QueryResearcherOrganisationType, QueryResult<ResearcherOrganisationType>>
    {
        private readonly IResearcherOrganisationTypeRepository _researcherOrganisationTypeRepository;
        
        
        public QueryResearcherOrganisationTypeHandler(IResearcherOrganisationTypeRepository researcherOrganisationTypeRepository)
        {
            _researcherOrganisationTypeRepository = researcherOrganisationTypeRepository;
        }
        
        public async Task<QueryResult<ResearcherOrganisationType>> Handle(QueryResearcherOrganisationType request, CancellationToken cancellationToken)
        {
            return await _researcherOrganisationTypeRepository.QueryResearcherOrganisationType(request.ApplyCustomFilters());
        }
    }
}

