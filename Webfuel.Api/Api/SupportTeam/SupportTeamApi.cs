using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    public static class SupportTeamApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            // Commands

            app.MapPost("api/support-team", Create)
                .RequireIdentity();

            app.MapPut("api/support-team", Update)
                .RequireIdentity();

            app.MapPut("api/support-team/sort", Sort)
                .RequireIdentity();

            app.MapDelete("api/support-team/{id:guid}", Delete)
                .RequireIdentity();

            // Querys

            app.MapPost("api/support-team/query", Query)
                .RequireIdentity();

            app.MapGet("api/support-team/{id:guid}", Get)
                .RequireIdentity();
        }

        public static Task<SupportTeam> Create([FromBody] CreateSupportTeam command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<SupportTeam> Update([FromBody] UpdateSupportTeam command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task Sort([FromBody] SortSupportTeam command, IMediator mediator)
        {
            return mediator.Send(command);
        }


        public static Task Delete(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteSupportTeam { Id = id });
        }

        public static Task<QueryResult<SupportTeam>> Query([FromBody] QuerySupportTeam command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static async Task<SupportTeam?> Get(Guid id, IMediator mediator)
        {
            return await mediator.Send(new GetSupportTeam { Id = id }) ;
        }
    }
}
