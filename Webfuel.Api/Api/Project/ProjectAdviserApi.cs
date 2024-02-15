using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain;
using Webfuel.Reporting;

namespace Webfuel.App
{
    [ApiService]
    public static class ProjectAdviserApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            // Commands

            app.MapPut("api/project-adviser/select-by-project-id/{projectId:guid}", SelectUserIdsByProjectId)
                .RequireIdentity();
        }

        public static async Task<List<Guid>> SelectUserIdsByProjectId(Guid projectId, [FromServices]IProjectAdviserService service)
        {
            return await service.SelectProjectAdviserUserIdsByProjectId(projectId);
        }
    }
}
