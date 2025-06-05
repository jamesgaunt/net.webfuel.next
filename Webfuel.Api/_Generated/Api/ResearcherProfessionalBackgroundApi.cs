using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class ResearcherProfessionalBackgroundApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Querys
            
            app.MapPost("api/researcher-professional-background/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<QueryResult<ResearcherProfessionalBackground>> Query([FromBody] QueryResearcherProfessionalBackground command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

