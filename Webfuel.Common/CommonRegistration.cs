using Microsoft.Extensions.DependencyInjection;

namespace Webfuel.Common
{
    public static class CommonRegistration
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            RepositoryRegistration.AddRepositoryServices(services);
            ServiceImplementation.Discover(typeof(CommonRegistration).Assembly, services);
        }
    }
}
