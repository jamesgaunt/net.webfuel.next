using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    public static class TitleApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Commands
            
            app.MapPost("api/title", CreateTitle)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/title", UpdateTitle)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/title/sort", SortTitle)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapDelete("api/title/{id:guid}", DeleteTitle)
                .RequireClaim(c => c.CanEditStaticData);
            
            // Querys
            
            app.MapPost("api/title/query", QueryTitle)
                .RequireIdentity();
        }
        
        public static Task<Title> CreateTitle([FromBody] CreateTitle command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task<Title> UpdateTitle([FromBody] UpdateTitle command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task SortTitle([FromBody] SortTitle command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task DeleteTitle(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteTitle { Id = id });
        }
        
        public static Task<QueryResult<Title>> QueryTitle([FromBody] QueryTitle command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

