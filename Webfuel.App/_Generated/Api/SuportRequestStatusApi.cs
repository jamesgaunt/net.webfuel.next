using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class SuportRequestStatusApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Querys
            
            app.MapPost("api/suport-request-status/query", Query)
                .RequireIdentity();
        }
        
        public static Task<QueryResult<SuportRequestStatus>> Query([FromBody] QuerySuportRequestStatus command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

