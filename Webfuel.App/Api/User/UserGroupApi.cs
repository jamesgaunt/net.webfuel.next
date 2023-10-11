using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain;

namespace Webfuel.App
{
    [ApiService]
    public static class UserGroupApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            // Commands

            app.MapPost("api/user-group", CreateUserGroup)
                .RequireClaim(c => c.CanEditUsers);

            app.MapPut("api/user-group", UpdateUserGroup)
                .RequireClaim(c => c.CanEditUsers);

            app.MapDelete("api/user-group/{id:guid}", DeleteUserGroup)
                .RequireClaim(c => c.CanEditUsers);

            // Querys

            app.MapPost("api/user-group/query", QueryUserGroup)
                .RequireIdentity();

            app.MapGet("api/user-group/{id:guid}", ResolveUserGroup)
                .RequireIdentity();
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
