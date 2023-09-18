using MediatR;

namespace Webfuel.Common
{
    public class QueryTenant : SearchQuery, IRequest<QueryResult<Tenant>>
    {
    }

    internal class QueryTenantHandler : IRequestHandler<QueryTenant, QueryResult<Tenant>>
    {
        private readonly ITenantRepository _tenantRepository;

        public QueryTenantHandler(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }

        public async Task<QueryResult<Tenant>> Handle(QueryTenant request, CancellationToken cancellationToken)
        {
            var q = request.ToRepositoryQuery();

            q.All(a => a.Contains(nameof(Tenant.Name), request?.Filter?.Search));

            return await _tenantRepository.QueryTenantAsync(q);
        }
    }
}
