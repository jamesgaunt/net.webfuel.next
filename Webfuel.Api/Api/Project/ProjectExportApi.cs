using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Common;
using Webfuel.Domain;
using Webfuel.Reporting;

namespace Webfuel.App
{
    [ApiService]
    public static class ProjectExportApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {

            app.MapPut("api/project-export", InitialiseReport)
                .RequireIdentity();
        }

        public static Task<ReportStep> InitialiseReport([FromBody] ProjectExportRequest request, IProjectExportService service)
        {
            return service.InitialiseReport(request);
        }
    }
}
