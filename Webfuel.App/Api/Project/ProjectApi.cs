using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    public static class ProjectApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            // Commands

            app.MapPost("api/project", Create)
                .RequireIdentity();

            app.MapPut("api/project", Update)
                .RequireIdentity();

            app.MapDelete("api/project/{id:guid}", Delete)
                .RequireIdentity();

            // Querys

            app.MapPost("api/project/query", Query)
                .RequireIdentity();

            app.MapGet("api/project/{id:guid}", Resolve)
                .RequireIdentity();
        }

        public static Task<Project> Create([FromBody] CreateProject command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<Project> Update([FromBody] UpdateProject command, IMediator mediator)
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

        public static async Task<Project> Resolve(Guid id, IMediator mediator)
        {
            return await mediator.Send(new GetProject { Id = id }) ?? throw new InvalidOperationException("The specified project does not exist");
        }
    }
}
