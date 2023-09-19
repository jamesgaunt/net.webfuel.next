using Microsoft.Extensions.DependencyInjection;
namespace Webfuel.Domain.Common
{
    internal static class RepositoryRegistration
    {
        public static void AddRepositoryServices(this IServiceCollection services)
        {
            services.AddSingleton<IJobRepository, JobRepository>();
            services.AddSingleton<IRepositoryAccessor<Job>, JobRepositoryAccessor>();
            services.AddSingleton<IRepositoryMapper<Job>, RepositoryDefaultMapper<Job>>();
            
            services.AddSingleton<ITenantRepository, TenantRepository>();
            services.AddSingleton<IRepositoryAccessor<Tenant>, TenantRepositoryAccessor>();
            services.AddSingleton<IRepositoryMapper<Tenant>, RepositoryDefaultMapper<Tenant>>();
            
            services.AddSingleton<ITenantDomainRepository, TenantDomainRepository>();
            services.AddSingleton<IRepositoryAccessor<TenantDomain>, TenantDomainRepositoryAccessor>();
            services.AddSingleton<IRepositoryMapper<TenantDomain>, RepositoryDefaultMapper<TenantDomain>>();
            
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IRepositoryAccessor<User>, UserRepositoryAccessor>();
            services.AddSingleton<IRepositoryMapper<User>, RepositoryDefaultMapper<User>>();
            
            services.AddSingleton<IUserGroupRepository, UserGroupRepository>();
            services.AddSingleton<IRepositoryAccessor<UserGroup>, UserGroupRepositoryAccessor>();
            services.AddSingleton<IRepositoryMapper<UserGroup>, RepositoryDefaultMapper<UserGroup>>();
            
            services.AddSingleton<IWidgetRepository, WidgetRepository>();
            services.AddSingleton<IRepositoryAccessor<Widget>, WidgetRepositoryAccessor>();
            services.AddSingleton<IRepositoryMapper<Widget>, RepositoryDefaultMapper<Widget>>();
            
            services.AddSingleton<IWidgetQueryViewRepository, WidgetQueryViewRepository>();
            services.AddSingleton<IRepositoryAccessor<WidgetQueryView>, WidgetQueryViewRepositoryAccessor>();
            services.AddSingleton<IRepositoryMapper<WidgetQueryView>, RepositoryDefaultMapper<WidgetQueryView>>();
            
        }
    }
}

