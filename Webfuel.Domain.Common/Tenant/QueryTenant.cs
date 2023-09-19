using Azure.Core;
using MediatR;

namespace Webfuel.Domain.Common
{
    public class QueryTenant : Query, IRequest<QueryResult<Tenant>>
    {
        public string Search { get; set; } = String.Empty;

        public override RepositoryQuery ToRepositoryQuery()
        {
            var q = base.ToRepositoryQuery();
            q.All(a => 
                a.Contains(nameof(Tenant.Name), Search));
            return q;
        }
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
            return await _tenantRepository.QueryTenantAsync(request.ToRepositoryQuery());
        }
    }
}
