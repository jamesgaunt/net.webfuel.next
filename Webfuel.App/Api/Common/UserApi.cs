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
            app.MapPost("api/create-user", CreateUser);

            app.MapPut("api/update-user", UpdateUser);

            app.MapDelete("api/delete-user/{id:guid}", DeleteUser)
                .AuthorizeClaim((c) => c.Developer);

            app.MapPost("api/query-user-list-view", QueryUserListView);

            app.MapGet("api/resolve-user/{id:guid}", ResolveUser);

            app.MapPost("api/login-user", LoginUser);
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

        public static Task<QueryResult<UserListView>> QueryUserListView([FromBody] QueryUserListView command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static async Task<User> ResolveUser(Guid id, IMediator mediator)
        {
            return await mediator.Send(new GetUser { Id = id }) ?? throw new InvalidOperationException("The specified user does not exist");
        }

        public static Task<IdentityToken> LoginUser([FromBody] LoginUser command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}
