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

            app.MapPut("api/project-support/resend-notification", ResendNotification)
                .RequireIdentity();

            app.MapPut("api/project-support/completion", UpdateCompletion)
                .RequireIdentity();

            app.MapPut("api/project-support/complete", Complete)
                 .RequireIdentity();

            app.MapPut("api/project-support/uncomplete", Uncomplete)
                     .RequireIdentity();

            app.MapDelete("api/project-support/{id:guid}", Delete)
                .RequireIdentity();

            // Querys

            app.MapPost("api/project-support/query", Query)
                .RequireIdentity();

            app.MapGet("api/project-support/{id:guid}", Get)
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

        public static Task<ProjectSupport> ResendNotification([FromBody] ResendProjectSupportNotification command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<ProjectSupport> UpdateCompletion([FromBody] UpdateProjectSupportCompletion command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<ProjectSupport> Complete([FromBody] CompleteProjectSupport command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<ProjectSupport> Uncomplete([FromBody] UncompleteProjectSupport command, IMediator mediator)
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

        public static async Task<ProjectSupport?> Get(Guid id, IMediator mediator)
        {
            return await mediator.Send(new GetProjectSupport { Id = id });
        }
    }
}
