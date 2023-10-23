using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class IsQuantativeTeamContributionApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Querys
            
            app.MapPost("api/is-quantative-team-contribution/query", Query)
                .RequireIdentity();
        }
        
        public static Task<QueryResult<IsQuantativeTeamContribution>> Query([FromBody] QueryIsQuantativeTeamContribution command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

