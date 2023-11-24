using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    public static class ReportGroupApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            // Commands
            
            app.MapPost("api/report-group", Create)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/report-group", Update)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/report-group/sort", Sort)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapDelete("api/report-group/{id:guid}", Delete)
                .RequireClaim(c => c.CanEditStaticData);
            
            // Querys
            
            app.MapPost("api/report-group/query", Query)
                .RequireIdentity();

            app.MapGet("api/report-group/{id:guid}", Get)
                .RequireIdentity();
        }
        
        public static Task<ReportGroup> Create([FromBody] CreateReportGroup command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task<ReportGroup> Update([FromBody] UpdateReportGroup command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Sort([FromBody] SortReportGroup command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Delete(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteReportGroup { Id = id });
        }
        
        public static Task<QueryResult<ReportGroup>> Query([FromBody] QueryReportGroup command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static async Task<ReportGroup?> Get(Guid id, IMediator mediator)
        {
            return await mediator.Send(new GetReportGroup { Id = id });
        }
    }
}

