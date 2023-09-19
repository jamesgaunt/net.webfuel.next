using Azure.Core;
using MediatR;

namespace Webfuel.Domain.Common
{
    public class QueryTenantDomain : Query, IRequest<QueryResult<TenantDomain>>
    {
        public Guid TenantId { get; set; }

        public string Search { get; set; } = String.Empty;

        public override RepositoryQuery ToRepositoryQuery()
        {
            var q = base.ToRepositoryQuery();
            q.All(a => a
                .Equal(nameof(TenantDomain.TenantId), TenantId));
            q.Any(a => a
                .Contains(nameof(TenantDomain.Domain), Search)
                .Contains(nameof(TenantDomain.RedirectTo), Search));
            return q;
        }
    }

    internal class QueryTenantDomainHandler : IRequestHandler<QueryTenantDomain, QueryResult<TenantDomain>>
    {
        private readonly ITenantDomainRepository _tenantDomainRepository;

        public QueryTenantDomainHandler(ITenantDomainRepository tenantDomainRepository)
        {
            _tenantDomainRepository = tenantDomainRepository;
        }

        public async Task<QueryResult<TenantDomain>> Handle(QueryTenantDomain request, CancellationToken cancellationToken)
        {
            return await _tenantDomainRepository.QueryTenantDomainAsync(request.ToRepositoryQuery());
        }
    }
}
