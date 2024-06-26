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
            
            // Querys
            
            app.MapPost("api/researcher-role/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<QueryResult<ResearcherRole>> Query([FromBody] QueryResearcherRole command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

