using FluentValidation;
using MediatR;

namespace Webfuel.Domain.Common
{
    public class ResolveTenant : IRequest<Tenant>
    {
        public Guid Id { get; set; }
    }

    internal class GetTenantHandler : IRequestHandler<ResolveTenant, Tenant>
    {
        private readonly ITenantRepository _tenantRepository;

        public GetTenantHandler(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }

        public async Task<Tenant> Handle(ResolveTenant request, CancellationToken cancellationToken)
        {
            return await _tenantRepository.RequireTenantAsync(request.Id);
        }
    }
}
