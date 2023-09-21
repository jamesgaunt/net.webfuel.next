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
            
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IRepositoryAccessor<User>, UserRepositoryAccessor>();
            services.AddSingleton<IRepositoryMapper<User>, RepositoryDefaultMapper<User>>();
            
            services.AddSingleton<IUserGroupRepository, UserGroupRepository>();
            services.AddSingleton<IRepositoryAccessor<UserGroup>, UserGroupRepositoryAccessor>();
            services.AddSingleton<IRepositoryMapper<UserGroup>, RepositoryDefaultMapper<UserGroup>>();
            
        }
    }
}

