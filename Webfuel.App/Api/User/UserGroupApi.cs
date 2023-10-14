using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    public static class UserGroupApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            // Commands

            app.MapPost("api/user-group", Create)
                .RequireClaim(c => c.CanEditUsers);

            app.MapPut("api/user-group", Update)
                .RequireClaim(c => c.CanEditUsers);

            app.MapDelete("api/user-group/{id:guid}", Delete)
                .RequireClaim(c => c.CanEditUsers);

            // Querys

            app.MapPost("api/user-group/query", Query)
                .RequireIdentity();

            app.MapGet("api/user-group/{id:guid}", Resolve)
                .RequireIdentity();
        }

        public static Task<UserGroup> Create([FromBody] CreateUserGroup command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<UserGroup> Update([FromBody] UpdateUserGroup command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task Delete(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteUserGroup { Id = id });
        }

        public static Task<QueryResult<UserGroup>> Query([FromBody] QueryUserGroup command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static async Task<UserGroup> Resolve(Guid id, IMediator mediator)
        {
            return await mediator.Send(new GetUserGroup { Id = id }) ?? throw new InvalidOperationException("The specified user group does not exist");
        }
    }
}
