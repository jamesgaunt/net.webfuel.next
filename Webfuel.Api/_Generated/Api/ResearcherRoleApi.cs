using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class ResearcherRoleApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Commands
            
            app.MapPost("api/researcher-role", Create)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/researcher-role", Update)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/researcher-role/sort", Sort)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapDelete("api/researcher-role/{id:guid}", Delete)
                .RequireClaim(c => c.CanEditStaticData);
            
            // Querys
            
            app.MapPost("api/researcher-role/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<ResearcherRole> Create([FromBody] CreateResearcherRole command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task<ResearcherRole> Update([FromBody] UpdateResearcherRole command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Sort([FromBody] SortResearcherRole command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Delete(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteResearcherRole { Id = id });
        }
        
        public static Task<QueryResult<ResearcherRole>> Query([FromBody] QueryResearcherRole command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

