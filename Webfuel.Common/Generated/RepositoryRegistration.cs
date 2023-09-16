using Microsoft.Extensions.DependencyInjection;
namespace Webfuel.Common
{
    internal static class RepositoryRegistration
    {
        public static void AddRepositoryServices(this IServiceCollection services)
        {
            services.AddSingleton<ITenantRepository, TenantRepository>();
            services.AddSingleton<IRepositoryAccessor<Tenant>, TenantRepositoryAccessor>();
            services.AddSingleton<IRepositoryMapper<Tenant>, RepositoryDefaultMapper<Tenant>>();
            
            services.AddSingleton<ITenantDomainRepository, TenantDomainRepository>();
            services.AddSingleton<IRepositoryAccessor<TenantDomain>, TenantDomainRepositoryAccessor>();
            services.AddSingleton<IRepositoryMapper<TenantDomain>, RepositoryDefaultMapper<TenantDomain>>();
            
        }
    }
}

