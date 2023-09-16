using Microsoft.Extensions.DependencyInjection;

namespace Webfuel.Common
{
    public static class CommonRegistration
    {
        public static void RegisterCommonServices(this IServiceCollection services)
        {
            RepositoryRegistration.AddRepositoryServices(services);

            services.RegisterServiceImplementationsFromAssembly(typeof(CommonRegistration).Assembly);
        }
    }
}
