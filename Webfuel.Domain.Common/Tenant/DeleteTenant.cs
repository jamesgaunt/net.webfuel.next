using MediatR;

namespace Webfuel.Domain.Common
{
    public class DeleteTenant : IRequest
    {
        public Guid Id { get; set; }
    }

    internal class DeleteTenantHandler : IRequestHandler<DeleteTenant>
    {
        private readonly ITenantRepository _tenantRepository;

        public DeleteTenantHandler(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }

        public async Task Handle(DeleteTenant request, CancellationToken cancellationToken)
        {
            await _tenantRepository.DeleteTenantAsync(request.Id);
        }
    }
}
