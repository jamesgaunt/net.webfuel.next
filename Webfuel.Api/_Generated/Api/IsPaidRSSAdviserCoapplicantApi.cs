using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class IsPaidRSSAdviserCoapplicantApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Querys
            
            app.MapPost("api/is-paid-rss-adviser-coapplicant/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<QueryResult<IsPaidRSSAdviserCoapplicant>> Query([FromBody] QueryIsPaidRSSAdviserCoapplicant command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

