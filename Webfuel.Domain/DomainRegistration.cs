using Microsoft.Extensions.DependencyInjection;
using System;
using Webfuel.Domain;

namespace Webfuel.Domain
{
    public class DomainAssemblyMarker { }

    public static class DomainRegistration
    {
        public static void RegisterDomainServices(this IServiceCollection services)
        {
            services.RegisterServicesFromAssembly(typeof(DomainRegistration).Assembly);

            services.RegisterValidatorsFromAssembly(typeof(DomainRegistration).Assembly);

            // Register Keyed Widget Data Providers 
            services.AddKeyedTransient<IWidgetDataProvider, ProjectSummaryProvider>(WidgetTypeEnum.ProjectSummary);
            services.AddKeyedTransient<IWidgetDataProvider, TeamSupportProvider>(WidgetTypeEnum.TeamSupport);
            services.AddKeyedTransient<IWidgetDataProvider, TeamActivityProvider>(WidgetTypeEnum.TeamActivity);
        }
    }
}
