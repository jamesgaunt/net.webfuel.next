using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QueryResearcherProfessionalBackground: Query, IRequest<QueryResult<ResearcherProfessionalBackground>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(ResearcherProfessionalBackground.Name), Search);
            return this;
        }
    }
    internal class QueryResearcherProfessionalBackgroundHandler : IRequestHandler<QueryResearcherProfessionalBackground, QueryResult<ResearcherProfessionalBackground>>
    {
        private readonly IResearcherProfessionalBackgroundRepository _researcherProfessionalBackgroundRepository;
        
        
        public QueryResearcherProfessionalBackgroundHandler(IResearcherProfessionalBackgroundRepository researcherProfessionalBackgroundRepository)
        {
            _researcherProfessionalBackgroundRepository = researcherProfessionalBackgroundRepository;
        }
        
        public async Task<QueryResult<ResearcherProfessionalBackground>> Handle(QueryResearcherProfessionalBackground request, CancellationToken cancellationToken)
        {
            return await _researcherProfessionalBackgroundRepository.QueryResearcherProfessionalBackground(request.ApplyCustomFilters());
        }
    }
}

