using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    public static class ProjectTeamSupportApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            // Commands

            app.MapPost("api/project-team-support", Create)
                .RequireIdentity();

            app.MapPut("api/project-team-support", Update)
                .RequireIdentity();

            app.MapPut("api/project-team-support/complete", Complete)
                .RequireIdentity();

            app.MapDelete("api/project-team-support/{id:guid}", Delete)
                .RequireIdentity();

            // Querys

            app.MapPost("api/project-team-support/query", Query)
                .RequireIdentity();

            app.MapGet("api/project-team-support/{id:guid}", Get)
                .RequireIdentity();
        }

        public static Task<ProjectTeamSupport> Create([FromBody] CreateProjectTeamSupport command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<ProjectTeamSupport> Update([FromBody] UpdateProjectTeamSupport command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<ProjectTeamSupport> Complete([FromBody] CompleteProjectTeamSupport command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task Delete(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteProjectTeamSupport { Id = id });
        }

        public static Task<QueryResult<ProjectTeamSupport>> Query([FromBody] QueryProjectTeamSupport command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static async Task<ProjectTeamSupport?> Get(Guid id, IMediator mediator)
        {
            return await mediator.Send(new GetProjectTeamSupport { Id = id });
        }
    }
}
