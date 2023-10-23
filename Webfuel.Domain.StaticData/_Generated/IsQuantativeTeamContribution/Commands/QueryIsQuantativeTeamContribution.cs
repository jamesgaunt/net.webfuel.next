using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QueryIsQuantativeTeamContribution: Query, IRequest<QueryResult<IsQuantativeTeamContribution>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(IsQuantativeTeamContribution.Name), Search);
            return this;
        }
    }
    internal class QueryIsQuantativeTeamContributionHandler : IRequestHandler<QueryIsQuantativeTeamContribution, QueryResult<IsQuantativeTeamContribution>>
    {
        private readonly IIsQuantativeTeamContributionRepository _isQuantativeTeamContributionRepository;
        
        
        public QueryIsQuantativeTeamContributionHandler(IIsQuantativeTeamContributionRepository isQuantativeTeamContributionRepository)
        {
            _isQuantativeTeamContributionRepository = isQuantativeTeamContributionRepository;
        }
        
        public async Task<QueryResult<IsQuantativeTeamContribution>> Handle(QueryIsQuantativeTeamContribution request, CancellationToken cancellationToken)
        {
            return await _isQuantativeTeamContributionRepository.QueryIsQuantativeTeamContribution(request.ApplyCustomFilters());
        }
    }
}

