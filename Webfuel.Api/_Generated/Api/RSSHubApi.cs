using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class RSSHubApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Querys
            
            app.MapPost("api/rss-hub/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<QueryResult<RSSHub>> Query([FromBody] QueryRSSHub command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

