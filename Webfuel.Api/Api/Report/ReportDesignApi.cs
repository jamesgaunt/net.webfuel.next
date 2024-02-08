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
            // Report Design Modification (note these have no authentication requirements as they don't save any state)

            app.MapPost("api/report-design/insert-column", InsertReportColumn);
            app.MapPost("api/report-design/update-column", UpdateReportColumn);
            app.MapPost("api/report-design/delete-column", DeleteReportColumn);

            app.MapPost("api/report-design/insert-filter", InsertReportFilter);
            app.MapPost("api/report-design/update-filter", UpdateReportFilter);
            app.MapPost("api/report-design/delete-filter", DeleteReportFilter);

            // Helpers

            app.MapGet("api/report-design/schema/{reportProviderId:guid}", GetReportSchema);
            app.MapPost("api/report-design/schema/lookup-reference-field", LookupReferenceField);
            app.MapPost("api/report-design/generate-arguments", GenerateArguments);
        }

        // Report Design Modification

        public static Task<ReportDesign> InsertReportColumn([FromBody] InsertReportColumn command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<ReportDesign> UpdateReportColumn([FromBody] UpdateReportColumn command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<ReportDesign> DeleteReportColumn([FromBody] DeleteReportColumn command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<ReportDesign> InsertReportFilter([FromBody] InsertReportFilter command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<ReportDesign> UpdateReportFilter([FromBody] UpdateReportFilter command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<ReportDesign> DeleteReportFilter([FromBody] DeleteReportFilter command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        // Helpers

        public static ReportSchema GetReportSchema(Guid reportProviderId, IReportDesignService reportDesignService)
        {
            return reportDesignService.GetReportSchema(reportProviderId);
        }

        public static Task<QueryResult<ReferenceLookup>> LookupReferenceField([FromBody] LookupReferenceField command, IReportDesignService reportDesignService)
        {
            return reportDesignService.QueryReferenceField(command.ReportProviderId, command.FieldId, command.Query);
        }

        public static Task<List<ReportArgument>> GenerateArguments([FromBody] ReportDesign design, IReportDesignService reportDesignService)
        {
            return reportDesignService.GenerateArguments(design);
        }
    }

    public class LookupReferenceField
    {
        public required Guid ReportProviderId { get; set; }

        public required Guid FieldId { get; set; }

        public required Query Query { get; set; }
    }
}
