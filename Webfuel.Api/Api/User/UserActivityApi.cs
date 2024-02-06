using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain;
using Webfuel.Reporting;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    public static class UserActivityApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            // Commands

            app.MapPost("api/user-activity", Create)
                .RequireIdentity();

            app.MapPut("api/user-activity", Update)
                .RequireIdentity();

            app.MapDelete("api/user-activity/{id:guid}", Delete)
                .RequireIdentity();

            // Querys

            app.MapPost("api/user-activity/query", Query)
                .RequireIdentity();

            app.MapGet("api/user-activity/{id:guid}", Get)
                .RequireIdentity();

            app.MapPut("api/user-activity/export", Export)
                   .RequireIdentity();
        }

        public static Task<UserActivity> Create([FromBody] CreateUserActivity command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<UserActivity> Update([FromBody] UpdateUserActivity command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task Delete(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteUserActivity { Id = id });
        }

        public static Task<QueryResult<UserActivity>> Query([FromBody] QueryUserActivity command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static async Task<UserActivity?> Get(Guid id, IMediator mediator)
        {
            return await mediator.Send(new GetUserActivity { Id = id });
        }

        public static Task<ReportStep> Export([FromBody] QueryUserActivity request, IExportUserActivityService service)
        {
            return service.InitialiseReport(request);
        }
    }
}
