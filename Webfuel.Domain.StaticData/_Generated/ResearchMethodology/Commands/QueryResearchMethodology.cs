using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QueryResearchMethodology: Query, IRequest<QueryResult<ResearchMethodology>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(ResearchMethodology.Name), Search);
            return this;
        }
    }
    internal class QueryResearchMethodologyHandler : IRequestHandler<QueryResearchMethodology, QueryResult<ResearchMethodology>>
    {
        private readonly IResearchMethodologyRepository _researchMethodologyRepository;
        
        
        public QueryResearchMethodologyHandler(IResearchMethodologyRepository researchMethodologyRepository)
        {
            _researchMethodologyRepository = researchMethodologyRepository;
        }
        
        public async Task<QueryResult<ResearchMethodology>> Handle(QueryResearchMethodology request, CancellationToken cancellationToken)
        {
            return await _researchMethodologyRepository.QueryResearchMethodology(request.ApplyCustomFilters());
        }
    }
}

