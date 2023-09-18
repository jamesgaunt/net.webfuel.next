using FluentValidation;
using MediatR;

namespace Webfuel.Common
{
    public class CreateTenant : IRequest<Tenant>
    {
        public string Name { get; set; } = String.Empty;

        public bool Live { get; set; }
    }

    internal class CreateTenantHandler : IRequestHandler<CreateTenant, Tenant>
    {
        private readonly ITenantRepository _tenantRepository;

        public CreateTenantHandler(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }

        public async Task<Tenant> Handle(CreateTenant request, CancellationToken cancellationToken)
        {
            return await _tenantRepository.InsertTenantAsync(new Tenant { Name = request.Name, Live = request.Live });
        }
    }
}
