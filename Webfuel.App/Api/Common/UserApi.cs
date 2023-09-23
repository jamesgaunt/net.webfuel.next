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
    public static class UserApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            // Commands

            app.MapPost("api/user", CreateUser)
                .RequireClaim(c => c.CanEditUsers);

            app.MapPut("api/user", UpdateUser)
                .RequireClaim(c => c.CanEditUsers);

            app.MapDelete("api/user/{id:guid}", DeleteUser)
                .RequireClaim(c => c.CanEditUsers);

            // Querys

            app.MapPost("api/user/query", QueryUser)
                .RequireIdentity();

            app.MapGet("api/user/{id:guid}", ResolveUser)
                .RequireIdentity();

            app.MapPost("api/user/login", LoginUser);
        }

        public static Task<User> CreateUser([FromBody] CreateUser command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<User> UpdateUser([FromBody] UpdateUser command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task DeleteUser(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteUser { Id = id });
        }

        public static Task<QueryResult<User>> QueryUser([FromBody] QueryUser command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static async Task<User> ResolveUser(Guid id, IMediator mediator)
        {
            return await mediator.Send(new GetUser { Id = id }) ?? throw new InvalidOperationException("The specified user does not exist");
        }

        public static Task<StringResult> LoginUser([FromBody] LoginUser command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}
