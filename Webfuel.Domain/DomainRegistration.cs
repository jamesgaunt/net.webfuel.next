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
            services.AddKeyedTransient<IWidgetProvider, LeadAdviserProjectsProvider>(WidgetTypeEnum.LeadAdviserProjects);
            services.AddKeyedTransient<IWidgetProvider, ProjectSummaryProvider>(WidgetTypeEnum.ProjectSummary);
            services.AddKeyedTransient<IWidgetProvider, SupportAdviserProjectsProvider>(WidgetTypeEnum.SupportAdviserProjects);
            services.AddKeyedTransient<IWidgetProvider, TeamSupportProvider>(WidgetTypeEnum.TeamSupport);
            services.AddKeyedTransient<IWidgetProvider, TeamActivityProvider>(WidgetTypeEnum.TeamActivity);
            services.AddKeyedTransient<IWidgetProvider, TriageSummaryProvider>(WidgetTypeEnum.TriageSummary);
        }
    }
}
