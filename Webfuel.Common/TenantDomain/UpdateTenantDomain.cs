using FluentValidation;
using MediatR;

namespace Webfuel.Common
{
    public class UpdateTenantDomain : IRequest<TenantDomain>
    {
        public Guid Id { get; set; }

        public string Domain { get; set; } = String.Empty;

        public string RedirectTo { get; set; } = String.Empty;
    }

    internal class UpdateTenantDomainHandler : IRequestHandler<UpdateTenantDomain, TenantDomain>
    {
        private readonly ITenantDomainRepository _tenantDomainRepository;

        public UpdateTenantDomainHandler(ITenantDomainRepository tenantDomainRepository)
        {
            _tenantDomainRepository = tenantDomainRepository;
        }

        public async Task<TenantDomain> Handle(UpdateTenantDomain request, CancellationToken cancellationToken)
        {
            var original = await _tenantDomainRepository.RequireTenantDomainAsync(request.Id);

            var updated = original.Copy();
            updated.Domain = request.Domain;
            updated.RedirectTo = request.RedirectTo;

            return await _tenantDomainRepository.UpdateTenantDomainAsync(original: original, updated: updated); ;
        }
    }
}
