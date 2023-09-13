using Microsoft.Extensions.DependencyInjection;

namespace Webfuel
{
    public static class CoreRegistration
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            RepositoryRegistration.AddRepositoryServices(services);
            ServiceImplementation.Discover(typeof(CoreRegistration).Assembly, services);
        }
    }
}
