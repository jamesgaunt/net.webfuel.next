using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class ResearchMethodologyApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Commands
            
            app.MapPost("api/research-methodology", Create)
                .RequireClaim(c => c.UserGroupClaims.CanEditStaticData);
            
            app.MapPut("api/research-methodology", Update)
                .RequireClaim(c => c.UserGroupClaims.CanEditStaticData);
            
            app.MapPut("api/research-methodology/sort", Sort)
                .RequireClaim(c => c.UserGroupClaims.CanEditStaticData);
            
            app.MapDelete("api/research-methodology/{id:guid}", Delete)
                .RequireClaim(c => c.UserGroupClaims.CanEditStaticData);
            
            // Querys
            
            app.MapPost("api/research-methodology/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<ResearchMethodology> Create([FromBody] CreateResearchMethodology command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task<ResearchMethodology> Update([FromBody] UpdateResearchMethodology command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Sort([FromBody] SortResearchMethodology command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Delete(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteResearchMethodology { Id = id });
        }
        
        public static Task<QueryResult<ResearchMethodology>> Query([FromBody] QueryResearchMethodology command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

