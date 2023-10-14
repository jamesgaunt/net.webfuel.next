using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    public static class TitleApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Commands
            
            app.MapPost("api/title", Create)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/title", Update)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/title/sort", Sort)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapDelete("api/title/{id:guid}", Delete)
                .RequireClaim(c => c.CanEditStaticData);
            
            // Querys
            
            app.MapPost("api/title/query", Query)
                .RequireIdentity();
        }
        
        public static Task<Title> Create([FromBody] CreateTitle command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task<Title> Update([FromBody] UpdateTitle command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Sort([FromBody] SortTitle command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Delete(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteTitle { Id = id });
        }
        
        public static Task<QueryResult<Title>> Query([FromBody] QueryTitle command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

