using Microsoft.Extensions.DependencyInjection;

namespace Webfuel.Common
{
    public static class CommonRegistration
    {
        public static void RegisterCommonServices(this IServiceCollection services)
        {
            RepositoryRegistration.AddRepositoryServices(services);

            services.RegisterServicesFromAssembly(typeof(CommonRegistration).Assembly);

            services.RegisterValidatorsFromAssembly(typeof(CoreRegistration).Assembly);

            services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<Webfuel.BlobStorage>());
        }
    }
}
