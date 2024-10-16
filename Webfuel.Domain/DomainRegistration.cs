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
            services.AddKeyedTransient<IWidgetDataProvider, IProjectSummaryProvider>(WidgetTypeEnum.ProjectSummary);
            services.AddKeyedTransient<IWidgetDataProvider, ITeamSupportProvider>(WidgetTypeEnum.TeamSupport);
            services.AddKeyedTransient<IWidgetDataProvider, ITeamActivityProvider>(WidgetTypeEnum.TeamActivity);
        }
    }
}
