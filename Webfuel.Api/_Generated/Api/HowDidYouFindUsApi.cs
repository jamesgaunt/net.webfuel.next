using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class HowDidYouFindUsApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Commands
            
            app.MapPost("api/how-did-you-find-us", Create)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/how-did-you-find-us", Update)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/how-did-you-find-us/sort", Sort)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapDelete("api/how-did-you-find-us/{id:guid}", Delete)
                .RequireClaim(c => c.CanEditStaticData);
            
            // Querys
            
            app.MapPost("api/how-did-you-find-us/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<HowDidYouFindUs> Create([FromBody] CreateHowDidYouFindUs command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task<HowDidYouFindUs> Update([FromBody] UpdateHowDidYouFindUs command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Sort([FromBody] SortHowDidYouFindUs command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Delete(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteHowDidYouFindUs { Id = id });
        }
        
        public static Task<QueryResult<HowDidYouFindUs>> Query([FromBody] QueryHowDidYouFindUs command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

