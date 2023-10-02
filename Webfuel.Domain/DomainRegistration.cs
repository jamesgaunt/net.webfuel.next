using Microsoft.Extensions.DependencyInjection;

namespace Webfuel.Domain
{
    public class DomainAssemblyMarker { }

    public static class DomainRegistration
    {
        public static void RegisterDomainServices(this IServiceCollection services)
        {
            services.RegisterServicesFromAssembly(typeof(DomainRegistration).Assembly);

            services.RegisterValidatorsFromAssembly(typeof(DomainRegistration).Assembly);
        }
    }
}
