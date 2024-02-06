using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain;
using Webfuel.Reporting;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    public static class ProjectApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            // Commands

            app.MapPut("api/project", Update)
                .RequireIdentity();

            app.MapPut("api/project/status", UpdateStatus)
                .RequireClaim(c => c.CanUnlockProjects);

            app.MapDelete("api/project/{id:guid}", Delete)
                .RequireIdentity();

            // Querys

            app.MapPost("api/project/query", Query)
                .RequireIdentity();

            app.MapGet("api/project/{id:guid}", Get)
                .RequireIdentity();

            app.MapPut("api/project/export", Export)
                .RequireIdentity();
        }

        public static Task<Project> Update([FromBody] UpdateProject command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<Project> UpdateStatus([FromBody] UpdateProjectStatus command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task Delete(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteProject { Id = id });
        }

        public static Task<QueryResult<Project>> Query([FromBody] QueryProject command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static async Task<Project?> Get(Guid id, IMediator mediator)
        {
            return await mediator.Send(new GetProject { Id = id });
        }

        public static Task<ReportStep> Export([FromBody] QueryProject request, IExportProjectService service)
        {
            return service.InitialiseReport(request);
        }
    }
}
