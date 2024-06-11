using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QueryResearcherLocation: Query, IRequest<QueryResult<ResearcherLocation>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(ResearcherLocation.Name), Search);
            return this;
        }
    }
    internal class QueryResearcherLocationHandler : IRequestHandler<QueryResearcherLocation, QueryResult<ResearcherLocation>>
    {
        private readonly IResearcherLocationRepository _researcherLocationRepository;
        
        
        public QueryResearcherLocationHandler(IResearcherLocationRepository researcherLocationRepository)
        {
            _researcherLocationRepository = researcherLocationRepository;
        }
        
        public async Task<QueryResult<ResearcherLocation>> Handle(QueryResearcherLocation request, CancellationToken cancellationToken)
        {
            return await _researcherLocationRepository.QueryResearcherLocation(request.ApplyCustomFilters());
        }
    }
}

