using Microsoft.Extensions.DependencyInjection;

namespace Webfuel.Common
{
    public class CommonAssemblyMarker { }

    public static class CommonRegistration
    {
        public static void RegisterCommonServices(this IServiceCollection services)
        {
            services.RegisterServicesFromAssembly(typeof(CommonRegistration).Assembly);

            services.RegisterValidatorsFromAssembly(typeof(CommonRegistration).Assembly);
        }
    }
}
