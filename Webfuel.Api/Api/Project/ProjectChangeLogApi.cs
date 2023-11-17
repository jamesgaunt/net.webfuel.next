using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    public static class ProjectChangeLogApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            // Querys

            app.MapPost("api/project-change-log/query", Query)
                .RequireIdentity();

        }

        public static Task<QueryResult<ProjectChangeLog>> Query([FromBody] QueryProjectChangeLog command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}
