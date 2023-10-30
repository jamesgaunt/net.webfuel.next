using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    public static class ProjectSubmissionApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            // Commands

            app.MapPost("api/project-submission", Create)
                .RequireIdentity();

            app.MapPut("api/project-submission", Update)
                .RequireIdentity();

            app.MapDelete("api/project-submission/{id:guid}", Delete)
                .RequireIdentity();

            // Querys

            app.MapPost("api/project-submission/query", Query)
                .RequireIdentity();

            app.MapGet("api/project-submission/{id:guid}", Get)
                .RequireIdentity();
        }

        public static Task<ProjectSubmission> Create([FromBody] CreateProjectSubmission command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<ProjectSubmission> Update([FromBody] UpdateProjectSubmission command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task Delete(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteProjectSubmission { Id = id });
        }

        public static Task<QueryResult<ProjectSubmission>> Query([FromBody] QueryProjectSubmission command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static async Task<ProjectSubmission?> Get(Guid id, IMediator mediator)
        {
            return await mediator.Send(new GetProjectSubmission { Id = id });
        }
    }
}
