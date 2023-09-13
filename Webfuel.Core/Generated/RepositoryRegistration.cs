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
            
            services.AddSingleton<IWidgetRepository, WidgetRepository>();
            services.AddSingleton<IRepositoryAccessor<Widget>, WidgetRepositoryAccessor>();
            services.AddSingleton<IRepositoryMapper<Widget>, RepositoryDefaultMapper<Widget>>();
            
        }
    }
}

