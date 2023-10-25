using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QuerySite: Query, IRequest<QueryResult<Site>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(Site.Name), Search);
            return this;
        }
    }
    internal class QuerySiteHandler : IRequestHandler<QuerySite, QueryResult<Site>>
    {
        private readonly ISiteRepository _siteRepository;
        
        
        public QuerySiteHandler(ISiteRepository siteRepository)
        {
            _siteRepository = siteRepository;
        }
        
        public async Task<QueryResult<Site>> Handle(QuerySite request, CancellationToken cancellationToken)
        {
            return await _siteRepository.QuerySite(request.ApplyCustomFilters());
        }
    }
}

