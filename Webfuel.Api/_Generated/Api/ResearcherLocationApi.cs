using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class ResearcherLocationApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Commands
            
            app.MapPost("api/researcher-location", Create)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/researcher-location", Update)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/researcher-location/sort", Sort)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapDelete("api/researcher-location/{id:guid}", Delete)
                .RequireClaim(c => c.CanEditStaticData);
            
            // Querys
            
            app.MapPost("api/researcher-location/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<ResearcherLocation> Create([FromBody] CreateResearcherLocation command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task<ResearcherLocation> Update([FromBody] UpdateResearcherLocation command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Sort([FromBody] SortResearcherLocation command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Delete(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteResearcherLocation { Id = id });
        }
        
        public static Task<QueryResult<ResearcherLocation>> Query([FromBody] QueryResearcherLocation command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

