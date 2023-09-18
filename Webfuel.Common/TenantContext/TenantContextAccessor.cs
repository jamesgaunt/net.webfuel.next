using Microsoft.AspNetCore.Http;

namespace Webfuel.Common
{
    public interface ITenantContextAccessor
    {
        Task<TenantContext?> GetCurrentAsync();
    }

    [ServiceImplementation(typeof(ITenantContextAccessor))]
    internal class TenantContextAccessor : ITenantContextAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITenantRepository _tenantRepository;
        private readonly ITenantDomainRepository _tenantDomainRepository;

        public TenantContextAccessor(
            IHttpContextAccessor httpContextAccessor, ITenantRepository tenantRepository, ITenantDomainRepository tenantDomainRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _tenantRepository = tenantRepository;
            _tenantDomainRepository = tenantDomainRepository;
        }

        const string StateKey = "TenantContext";

        public async Task<TenantContext?> GetCurrentAsync()
        {
            var tenantContext = _httpContextAccessor.GetState<TenantContext>(StateKey);
            if (tenantContext != null)
                return tenantContext;

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
                return null;

            Tenant? tenant = null;

            var tenantDomain = await _tenantDomainRepository.GetTenantDomainByDomainAsync(httpContext.Request.Scheme + "://" + httpContext.Request.Host);
            if(tenantDomain != null)
                tenant = await _tenantRepository.GetTenantAsync(tenantDomain.TenantId);

            if (tenant == null)
                tenant = (await _tenantRepository.SelectTenantAsync()).FirstOrDefault();

            if (tenant == null)
                throw new InvalidOperationException("Unable to resolve tenant context");

            tenantContext = new TenantContext(tenant);

            _httpContextAccessor.SetState(StateKey, tenantContext);
            return tenantContext;
        }
    }
}
