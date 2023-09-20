using FluentValidation;
using MediatR;

namespace Webfuel.Domain.Common
{
    public class GetTenant : IRequest<Tenant?>
    {
        public Guid Id { get; set; }
    }

    internal class GetTenantHandler : IRequestHandler<GetTenant, Tenant?>
    {
        private readonly ITenantRepository _tenantRepository;

        public GetTenantHandler(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }

        public async Task<Tenant?> Handle(GetTenant request, CancellationToken cancellationToken)
        {
            return await _tenantRepository.GetTenantAsync(request.Id);
        }
    }
}
