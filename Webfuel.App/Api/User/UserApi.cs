using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    public static class UserApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            // Commands

            app.MapPost("api/user", Create)
                .RequireClaim(c => c.CanEditUsers);

            app.MapPut("api/user", Update)
                .RequireClaim(c => c.CanEditUsers);

            app.MapDelete("api/user/{id:guid}", Delete)
                .RequireClaim(c => c.CanEditUsers);

            // Querys

            app.MapPost("api/user/query", Query)
                .RequireIdentity();

            app.MapGet("api/user/{id:guid}", Resolve)
                .RequireIdentity();

            app.MapPost("api/user/login", Login);
        }

        public static Task<User> Create([FromBody] CreateUser command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<User> Update([FromBody] UpdateUser command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task Delete(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteUser { Id = id });
        }

        public static Task<QueryResult<User>> Query([FromBody] QueryUser command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static async Task<User> Resolve(Guid id, IMediator mediator)
        {
            return await mediator.Send(new GetUser { Id = id }) ?? throw new InvalidOperationException("The specified user does not exist");
        }

        public static Task<StringResult> Login([FromBody] LoginUser command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}
