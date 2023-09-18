using FluentValidation;
using MediatR;

namespace Webfuel.Common
{
    public class UpdateTenant : IRequest<Tenant>
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public bool Live { get; set; }
    }

    internal class UpdateTenantHandler : IRequestHandler<UpdateTenant, Tenant>
    {
        private readonly ITenantRepository _tenantRepository;

        public UpdateTenantHandler(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }

        public async Task<Tenant> Handle(UpdateTenant request, CancellationToken cancellationToken)
        {
            var original = await _tenantRepository.RequireTenantAsync(request.Id);

            var updated = original.Copy();
            updated.Name = request.Name;
            updated.Live = request.Live;

            return await _tenantRepository.UpdateTenantAsync(original: original, updated: updated); ;
        }
    }
}
