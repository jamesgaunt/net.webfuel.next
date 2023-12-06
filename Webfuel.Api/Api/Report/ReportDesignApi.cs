using MediatR;
using Microsoft.AspNetCore.Mvc;
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

            app.MapPost("api/report-design/get-report-reference", GetReportReference);

            app.MapPost("api/report-design/query-report-reference", QueryReportReference);
        }

        public static Task<ReportSchema> GetReportSchema(Guid reportProviderId, IReportDesignService reportDesignService)
        {
            return reportDesignService.GetReportSchema(reportProviderId);
        }

        public static Task<ReportReference?> GetReportReference([FromBody]GetReportReference command, IReportDesignService reportDesignService)
        {
            return reportDesignService.GetReportReference(
                reportProviderId: command.ReportProviderId,
                fieldId: command.FieldId,
                id: command.Id);
        }

        public static Task<QueryResult<ReportReference>> QueryReportReference([FromBody] QueryReportReference command, IReportDesignService reportDesignService)
        {
            return reportDesignService.QueryReportReference(
                reportProviderId: command.ReportProviderId,
                fieldId: command.FieldId,
                query: command.Query);
        }
    }

    public class GetReportReference
    {
        public required Guid ReportProviderId { get; set; }

        public required Guid FieldId { get; set; }

        public required Guid Id { get; set; }
    }

    public class QueryReportReference
    {
        public required Guid ReportProviderId { get; set; }

        public required Guid FieldId { get; set; }

        public required Query Query { get; set; }
    }
}
