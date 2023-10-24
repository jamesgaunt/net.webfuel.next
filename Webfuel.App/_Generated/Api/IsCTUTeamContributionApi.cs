using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class IsCTUTeamContributionApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Querys
            
            app.MapPost("api/is-ctu-team-contribution/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<QueryResult<IsCTUTeamContribution>> Query([FromBody] QueryIsCTUTeamContribution command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

