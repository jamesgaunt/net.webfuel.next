using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain;

namespace Webfuel.App
{
    [ApiService]
    public static class ProjectApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            // Commands

            app.MapPost("api/project", CreateProject)
                .RequireIdentity();

            app.MapPut("api/project", UpdateProject)
                .RequireIdentity();

            app.MapDelete("api/project/{id:guid}", DeleteProject)
                .RequireIdentity();

            // Querys

            app.MapPost("api/project/query", QueryProject)
                .RequireIdentity();

            app.MapGet("api/project/{id:guid}", ResolveProject)
                .RequireIdentity();
        }

        public static Task<Project> CreateProject([FromBody] CreateProject command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<Project> UpdateProject([FromBody] UpdateProject command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task DeleteProject(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteProject { Id = id });
        }

        public static Task<QueryResult<Project>> QueryProject([FromBody] QueryProject command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static async Task<Project> ResolveProject(Guid id, IMediator mediator)
        {
            return await mediator.Send(new GetProject { Id = id }) ?? throw new InvalidOperationException("The specified project does not exist");
        }
    }
}
