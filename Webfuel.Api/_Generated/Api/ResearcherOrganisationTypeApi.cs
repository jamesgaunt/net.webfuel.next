using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class ResearcherOrganisationTypeApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Commands
            
            app.MapPost("api/researcher-organisation-type", Create)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/researcher-organisation-type", Update)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/researcher-organisation-type/sort", Sort)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapDelete("api/researcher-organisation-type/{id:guid}", Delete)
                .RequireClaim(c => c.CanEditStaticData);
            
            // Querys
            
            app.MapPost("api/researcher-organisation-type/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<ResearcherOrganisationType> Create([FromBody] CreateResearcherOrganisationType command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task<ResearcherOrganisationType> Update([FromBody] UpdateResearcherOrganisationType command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Sort([FromBody] SortResearcherOrganisationType command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Delete(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteResearcherOrganisationType { Id = id });
        }
        
        public static Task<QueryResult<ResearcherOrganisationType>> Query([FromBody] QueryResearcherOrganisationType command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

