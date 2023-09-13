using Microsoft.Extensions.DependencyInjection;
namespace Webfuel
{
    internal static class RepositoryRegistration
    {
        public static void AddRepositoryServices(this IServiceCollection services)
        {
            services.AddSingleton<IEventLogRepository, EventLogRepository>();
            services.AddSingleton<IRepositoryAccessor<EventLog>, EventLogRepositoryAccessor>();
            services.AddSingleton<IRepositoryMapper<EventLog>, RepositoryDefaultMapper<EventLog>>();
            
        }
    }
}

