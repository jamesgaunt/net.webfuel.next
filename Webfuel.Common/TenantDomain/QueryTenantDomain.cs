using MediatR;

namespace Webfuel.Common
{
    public class QueryTenantDomain : SearchQuery, IRequest<QueryResult<TenantDomain>>
    {
        public Guid TenantId { get; set; }
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
            var q = request.ToRepositoryQuery();

            q.All(a => a
                .Equal(nameof(TenantDomain.TenantId), request.TenantId));

            q.Any(a => a
                .Contains(nameof(TenantDomain.Domain), request?.Filter?.Search)
                .Contains(nameof(TenantDomain.RedirectTo), request?.Filter?.Search));

            return await _tenantDomainRepository.QueryTenantDomainAsync(q);
        }
    }
}
