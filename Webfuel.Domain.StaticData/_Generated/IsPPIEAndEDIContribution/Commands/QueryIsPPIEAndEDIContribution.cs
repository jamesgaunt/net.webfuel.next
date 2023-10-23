using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QueryIsPPIEAndEDIContribution: Query, IRequest<QueryResult<IsPPIEAndEDIContribution>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(IsPPIEAndEDIContribution.Name), Search);
            return this;
        }
    }
    internal class QueryIsPPIEAndEDIContributionHandler : IRequestHandler<QueryIsPPIEAndEDIContribution, QueryResult<IsPPIEAndEDIContribution>>
    {
        private readonly IIsPPIEAndEDIContributionRepository _isPPIEAndEDIContributionRepository;
        
        
        public QueryIsPPIEAndEDIContributionHandler(IIsPPIEAndEDIContributionRepository isPPIEAndEDIContributionRepository)
        {
            _isPPIEAndEDIContributionRepository = isPPIEAndEDIContributionRepository;
        }
        
        public async Task<QueryResult<IsPPIEAndEDIContribution>> Handle(QueryIsPPIEAndEDIContribution request, CancellationToken cancellationToken)
        {
            return await _isPPIEAndEDIContributionRepository.QueryIsPPIEAndEDIContribution(request.ApplyCustomFilters());
        }
    }
}

