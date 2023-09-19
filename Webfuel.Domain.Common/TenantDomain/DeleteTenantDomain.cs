using MediatR;

namespace Webfuel.Domain.Common
{
    public class DeleteTenantDomain : IRequest
    {
        public Guid Id { get; set; }
    }

    internal class DeleteTenantDomainHandler : IRequestHandler<DeleteTenantDomain>
    {
        private readonly ITenantDomainRepository _tenantDomainRepository;

        public DeleteTenantDomainHandler(ITenantDomainRepository tenantDomainRepository)
        {
            _tenantDomainRepository = tenantDomainRepository;
        }

        public async Task Handle(DeleteTenantDomain request, CancellationToken cancellationToken)
        {
            await _tenantDomainRepository.DeleteTenantDomainAsync(request.Id);
        }
    }
}
