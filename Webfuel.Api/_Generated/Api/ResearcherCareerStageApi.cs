using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class ResearcherCareerStageApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Commands
            
            app.MapPost("api/researcher-career-stage", Create)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/researcher-career-stage", Update)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/researcher-career-stage/sort", Sort)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapDelete("api/researcher-career-stage/{id:guid}", Delete)
                .RequireClaim(c => c.CanEditStaticData);
            
            // Querys
            
            app.MapPost("api/researcher-career-stage/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<ResearcherCareerStage> Create([FromBody] CreateResearcherCareerStage command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task<ResearcherCareerStage> Update([FromBody] UpdateResearcherCareerStage command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Sort([FromBody] SortResearcherCareerStage command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Delete(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteResearcherCareerStage { Id = id });
        }
        
        public static Task<QueryResult<ResearcherCareerStage>> Query([FromBody] QueryResearcherCareerStage command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

