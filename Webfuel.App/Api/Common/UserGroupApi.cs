using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Threading;
using Webfuel.Domain.Common;

namespace Webfuel.App
{
    [ApiService]
    public static class UserGroupApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            app.MapPost("api/create-user-group", CreateUserGroup);

            app.MapPut("api/update-user-group", UpdateUserGroup);

            app.MapDelete("api/delete-user-group/{id:guid}", DeleteUserGroup)
                .AuthorizeClaim((c) => c.Developer);

            app.MapPost("api/query-user-group", QueryUserGroup);

            app.MapGet("api/resolve-user-group/{id:guid}", ResolveUserGroup);
        }

        public static Task<UserGroup> CreateUserGroup([FromBody] CreateUserGroup command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<UserGroup> UpdateUserGroup([FromBody] UpdateUserGroup command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task DeleteUserGroup(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteUserGroup { Id = id });
        }

        public static Task<QueryResult<UserGroup>> QueryUserGroup([FromBody] QueryUserGroup command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static async Task<UserGroup> ResolveUserGroup(Guid id, IMediator mediator)
        {
            return await mediator.Send(new GetUserGroup { Id = id }) ?? throw new InvalidOperationException("The specified user group does not exist");
        }
    }
}
