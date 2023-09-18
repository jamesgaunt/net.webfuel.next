using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Threading;
using Webfuel.Common;

namespace Webfuel.App
{
    [ApiService]
    public static class TenantApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            app.MapPost("api/create-tenant", CreateTenant);

            app.MapPut("api/update-tenant", UpdateTenant);

            app.MapDelete("api/delete-tenant/{id:guid}", DeleteTenant);

            app.MapPost("api/query-tenant", QueryTenant);
        }

        public static Task<Tenant> CreateTenant([FromBody] CreateTenant command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<Tenant> UpdateTenant([FromBody] UpdateTenant command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task DeleteTenant(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteTenant { Id = id });
        }

        public static Task<QueryResult<Tenant>> QueryTenant([FromBody] QueryTenant command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}
