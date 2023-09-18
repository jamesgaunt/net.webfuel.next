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
    public static class TenantDomainApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            app.MapPost("api/create-tenant-domain", CreateTenantDomain);

            app.MapPut("api/update-tenant-domain", UpdateTenantDomain);

            app.MapDelete("api/delete-tenant-domain/{id:guid}", DeleteTenantDomain);

            app.MapPost("api/query-tenant-domain", QueryTenantDomain);
        }

        public static Task<TenantDomain> CreateTenantDomain([FromBody] CreateTenantDomain command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<TenantDomain> UpdateTenantDomain([FromBody] UpdateTenantDomain command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task DeleteTenantDomain(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteTenantDomain { Id = id });
        }

        public static Task<QueryResult<TenantDomain>> QueryTenantDomain([FromBody] QueryTenantDomain command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}
