using Microsoft.Extensions.DependencyInjection;

namespace Webfuel
{
    public static class CoreRegistration
    {
        public static void RegisterCoreServices(this IServiceCollection services)
        {
            RepositoryRegistration.AddRepositoryServices(services);

            services.RegisterServiceImplementationsFromAssembly(typeof(CoreRegistration).Assembly);
        }
    }
}
