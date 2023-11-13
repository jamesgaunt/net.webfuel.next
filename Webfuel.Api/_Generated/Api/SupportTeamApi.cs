using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class SupportTeamApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Querys
            
            app.MapPost("api/support-team/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<QueryResult<SupportTeam>> Query([FromBody] QuerySupportTeam command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

