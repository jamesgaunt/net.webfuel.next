using FluentValidation;
using MediatR;

namespace Webfuel.Domain.Common
{
    public class CreateTenantDomain : IRequest<TenantDomain>
    {
        public Guid TenantId { get; set; }

        public string Domain { get; set; } = String.Empty;

        public string RedirectTo { get; set; } = String.Empty;
    }

    internal class CreateTenantDomainHandler : IRequestHandler<CreateTenantDomain, TenantDomain>
    {
        private readonly ITenantDomainRepository _tenantDomainRepository;

        public CreateTenantDomainHandler(ITenantDomainRepository tenantDomainRepository)
        {
            _tenantDomainRepository = tenantDomainRepository;
        }

        public async Task<TenantDomain> Handle(CreateTenantDomain request, CancellationToken cancellationToken)
        {
            return await _tenantDomainRepository.InsertTenantDomainAsync(new TenantDomain { 
                TenantId = request.TenantId,
                Domain = request.Domain, 
                RedirectTo = request.RedirectTo 
            });
        }
    }
}
