using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class IsPaidRSSAdviserLeadApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Querys
            
            app.MapPost("api/is-paid-rss-adviser-lead/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<QueryResult<IsPaidRSSAdviserLead>> Query([FromBody] QueryIsPaidRSSAdviserLead command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

