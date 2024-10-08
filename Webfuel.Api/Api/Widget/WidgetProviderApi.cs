using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    public static class WidgetProviderApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            // Commands

            app.MapGet("api/widget/project-summary/{id:guid}", ProjectSummary)
                .RequireIdentity();

            app.MapGet("api/widget/team-support/{id:guid}", TeamSupport)
                .RequireIdentity();
        }

        public static Task<ProjectSummaryData> ProjectSummary(Guid id, [FromServices]IProjectSummaryProvider provider)
        {
            return provider.GetData(id);
        }

        public static Task<TeamSupportData> TeamSupport(Guid id, [FromServices] ITeamSupportProvider provider)
        {
            return provider.GetData(id);
        }
    }
}
