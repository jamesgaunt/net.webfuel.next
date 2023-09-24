using Microsoft.Extensions.DependencyInjection;

namespace Webfuel.Domain
{
    public class CommonAssemblyMarker { }

    public static class CommonRegistration
    {
        public static void RegisterCommonServices(this IServiceCollection services)
        {
            RepositoryRegistration.AddRepositoryServices(services);

            services.RegisterServicesFromAssembly(typeof(CommonRegistration).Assembly);

            services.RegisterValidatorsFromAssembly(typeof(CoreRegistration).Assembly);
        }
    }
}
