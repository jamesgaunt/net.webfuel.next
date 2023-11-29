using MediatR;
using Webfuel.Domain;

namespace Webfuel.App
{
    [ApiService]
    public static class ReportDesignApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            app.MapGet("api/report-design/schema/{reportProviderId:guid}", GetReportSchema);
        }

        public static Task<IReportSchema> GetReportSchema(Guid reportProviderId, IReportService reportService)
        {
            return reportService.GetReportSchema(reportProviderId);
        }
    }
}
