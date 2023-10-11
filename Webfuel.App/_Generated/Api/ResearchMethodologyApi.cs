using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    public static class ResearchMethodologyApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Commands
            
            app.MapPost("api/research-methodology", CreateResearchMethodology)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/research-methodology", UpdateResearchMethodology)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/research-methodology/sort", SortResearchMethodology)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapDelete("api/research-methodology/{id:guid}", DeleteResearchMethodology)
                .RequireClaim(c => c.CanEditStaticData);
            
            // Querys
            
            app.MapPost("api/research-methodology/query", QueryResearchMethodology)
                .RequireIdentity();
        }
        
        public static Task<ResearchMethodology> CreateResearchMethodology([FromBody] CreateResearchMethodology command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task<ResearchMethodology> UpdateResearchMethodology([FromBody] UpdateResearchMethodology command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task SortResearchMethodology([FromBody] SortResearchMethodology command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task DeleteResearchMethodology(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteResearchMethodology { Id = id });
        }
        
        public static Task<QueryResult<ResearchMethodology>> QueryResearchMethodology([FromBody] QueryResearchMethodology command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

