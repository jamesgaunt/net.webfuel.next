using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QueryResearcherCareerStage: Query, IRequest<QueryResult<ResearcherCareerStage>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(ResearcherCareerStage.Name), Search);
            return this;
        }
    }
    internal class QueryResearcherCareerStageHandler : IRequestHandler<QueryResearcherCareerStage, QueryResult<ResearcherCareerStage>>
    {
        private readonly IResearcherCareerStageRepository _researcherCareerStageRepository;
        
        
        public QueryResearcherCareerStageHandler(IResearcherCareerStageRepository researcherCareerStageRepository)
        {
            _researcherCareerStageRepository = researcherCareerStageRepository;
        }
        
        public async Task<QueryResult<ResearcherCareerStage>> Handle(QueryResearcherCareerStage request, CancellationToken cancellationToken)
        {
            return await _researcherCareerStageRepository.QueryResearcherCareerStage(request.ApplyCustomFilters());
        }
    }
}

