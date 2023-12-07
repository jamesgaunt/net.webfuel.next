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
            //app.MapPost("api/report-design/update-filter", UpdateReportFilter);
            app.MapPost("api/report-design/delete-filter", DeleteReportFilter);

            // Helpers

            app.MapGet("api/report-design/schema/{reportProviderId:guid}", GetReportSchema);
            app.MapPost("api/report-design/get-report-reference", GetReportReference);
            app.MapPost("api/report-design/query-report-reference", QueryReportReference);
            app.MapPost("api/report-design/validate-design", ValidateDesign);
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

        public static Task<ReportDesign> DeleteReportFilter([FromBody] DeleteReportFilter command, IMediator mediator)
        {
            return mediator.Send(command);
        }


        // Helpers

        public static ReportSchema GetReportSchema(Guid reportProviderId, IReportDesignService reportDesignService)
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

        public static ReportDesign ValidateDesign([FromBody] ValidateDesign command, IReportDesignService reportDesignService)
        {
            reportDesignService.ValidateDesign(
                reportProviderId: command.ReportProviderId,
                design: command.Design);
            
            return command.Design;
        }
    }




    // TODO: Get rid of these!

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

    public class ValidateDesign
    {
        public required Guid ReportProviderId { get; set; }

        public required ReportDesign Design { get; init; }
    }
}
