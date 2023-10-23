using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    public static class ProjectSupportApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            // Commands

            app.MapPost("api/project-support", Create)
                .RequireIdentity();

            app.MapPut("api/project-support", Update)
                .RequireIdentity();

            app.MapDelete("api/project-support/{id:guid}", Delete)
                .RequireIdentity();

            // Querys

            app.MapPost("api/project-support/query", Query)
                .RequireIdentity();

            app.MapGet("api/project-support/{id:guid}", Resolve)
                .RequireIdentity();
        }

        public static Task<ProjectSupport> Create([FromBody] CreateProjectSupport command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<ProjectSupport> Update([FromBody] UpdateProjectSupport command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task Delete(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteProjectSupport { Id = id });
        }

        public static Task<QueryResult<ProjectSupport>> Query([FromBody] QueryProjectSupport command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static async Task<ProjectSupport> Resolve(Guid id, IMediator mediator)
        {
            return await mediator.Send(new GetProjectSupport { Id = id }) ?? throw new InvalidOperationException("The specified project support does not exist");
        }
    }
}
