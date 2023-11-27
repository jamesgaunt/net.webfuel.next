using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Common;
using Webfuel.Domain;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    public static class ReportApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            app.MapPut("api/report/{taskId:guid}", Generate)
                .RequireIdentity();
        }

        public static Task<ReportProgress> Generate(Guid taskId, IReportService service)
        {
            return service.GenerateReport(taskId);
        }
    }
}
