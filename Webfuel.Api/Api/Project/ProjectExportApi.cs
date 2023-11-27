using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Common;
using Webfuel.Domain;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    public static class ProjectExportApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {

            app.MapPut("api/project-export", Initialise)
                .RequireIdentity();
        }

        public static Task<ReportProgress> Initialise([FromBody] ProjectExportRequest request, IProjectExportService service)
        {
            return service.InitialiseReport(request);
        }
    }
}
