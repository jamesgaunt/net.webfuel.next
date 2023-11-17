using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    public static class SupportRequestChangeLogApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            // Querys

            app.MapPost("api/support-request-change-log/query", Query)
                .RequireIdentity();

        }

        public static Task<QueryResult<SupportRequestChangeLog>> Query([FromBody] QuerySupportRequestChangeLog command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}
