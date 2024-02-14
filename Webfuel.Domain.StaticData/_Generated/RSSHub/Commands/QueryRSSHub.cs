using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QueryRSSHub: Query, IRequest<QueryResult<RSSHub>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(RSSHub.Name), Search);
            return this;
        }
    }
    internal class QueryRSSHubHandler : IRequestHandler<QueryRSSHub, QueryResult<RSSHub>>
    {
        private readonly IRSSHubRepository _rsshubRepository;
        
        
        public QueryRSSHubHandler(IRSSHubRepository rsshubRepository)
        {
            _rsshubRepository = rsshubRepository;
        }
        
        public async Task<QueryResult<RSSHub>> Handle(QueryRSSHub request, CancellationToken cancellationToken)
        {
            return await _rsshubRepository.QueryRSSHub(request.ApplyCustomFilters());
        }
    }
}

