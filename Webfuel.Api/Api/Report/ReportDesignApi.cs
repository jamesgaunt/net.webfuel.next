using MediatR;
using Webfuel.Domain;
using Webfuel.Reporting;

namespace Webfuel.App
{
    [ApiService]
    public static class ReportDesignApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            app.MapGet("api/report-design/schema/{reportProviderId:guid}", GetReportSchema);
        }

        public static Task<ReportSchema> GetReportSchema(Guid reportProviderId, IReportProviderService reportProviderService)
        {
            return reportProviderService.GetReportSchema(reportProviderId);
        }
    }
}
