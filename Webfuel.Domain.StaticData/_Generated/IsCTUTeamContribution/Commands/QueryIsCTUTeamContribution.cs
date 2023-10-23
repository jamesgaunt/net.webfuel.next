using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QueryIsCTUTeamContribution: Query, IRequest<QueryResult<IsCTUTeamContribution>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(IsCTUTeamContribution.Name), Search);
            return this;
        }
    }
    internal class QueryIsCTUTeamContributionHandler : IRequestHandler<QueryIsCTUTeamContribution, QueryResult<IsCTUTeamContribution>>
    {
        private readonly IIsCTUTeamContributionRepository _isCTUTeamContributionRepository;
        
        
        public QueryIsCTUTeamContributionHandler(IIsCTUTeamContributionRepository isCTUTeamContributionRepository)
        {
            _isCTUTeamContributionRepository = isCTUTeamContributionRepository;
        }
        
        public async Task<QueryResult<IsCTUTeamContribution>> Handle(QueryIsCTUTeamContribution request, CancellationToken cancellationToken)
        {
            return await _isCTUTeamContributionRepository.QueryIsCTUTeamContribution(request.ApplyCustomFilters());
        }
    }
}

