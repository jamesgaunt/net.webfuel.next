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
            
            // Commands
            
            app.MapPost("api/rss-hub", Create)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/rss-hub", Update)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/rss-hub/sort", Sort)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapDelete("api/rss-hub/{id:guid}", Delete)
                .RequireClaim(c => c.CanEditStaticData);
            
            // Querys
            
            app.MapPost("api/rss-hub/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<RSSHub> Create([FromBody] CreateRSSHub command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task<RSSHub> Update([FromBody] UpdateRSSHub command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Sort([FromBody] SortRSSHub command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Delete(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteRSSHub { Id = id });
        }
        
        public static Task<QueryResult<RSSHub>> Query([FromBody] QueryRSSHub command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

