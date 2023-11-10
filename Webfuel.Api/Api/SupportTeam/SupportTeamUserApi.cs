using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    public static class SupportTeamUserApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            // Commands

            app.MapPost("api/support-team-user/insert", Insert)
                .RequireIdentity();

            app.MapPost("api/support-team-user/delete", Delete)
                .RequireIdentity();

            // Querys

            app.MapPost("api/support-team-user/query", Query)
                .RequireIdentity();
        }

        public static Task Insert([FromBody] InsertSupportTeamUser command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task Delete([FromBody] DeleteSupportTeamUser command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<QueryResult<SupportTeamUser>> Query([FromBody] QuerySupportTeamUser command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}
