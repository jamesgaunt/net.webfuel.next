using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class SupportRequestStatusApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Querys
            
            app.MapPost("api/support-request-status/query", Query)
                .RequireIdentity();
        }
        
        public static Task<QueryResult<SupportRequestStatus>> Query([FromBody] QuerySupportRequestStatus command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

