using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    public static class UserLoginApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            // Commands

            app.MapPost("api/user/login", Login);

            app.MapPost("api/user/change-password", ChangePassword)
                .RequireIdentity();

            app.MapPost("api/user/update-password", UpdatePassword)
                .RequireIdentity();

            app.MapPost("api/user/send-password-reset-email", SendPasswordResetEmail);

            app.MapPost("api/user/reset-password", ResetPassword);

            // Querys

            app.MapPost("api/user/login/query", Query)
                .RequireIdentity();
        }

        public static Task<StringResult> Login([FromBody] LoginUser command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task ChangePassword([FromBody] ChangeUserPassword command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task UpdatePassword([FromBody] UpdateUserPassword command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task SendPasswordResetEmail([FromBody] SendUserPasswordResetEmail command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task ResetPassword([FromBody] ResetUserPassword command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<QueryResult<UserLogin>> Query([FromBody] QueryUserLogin command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}
