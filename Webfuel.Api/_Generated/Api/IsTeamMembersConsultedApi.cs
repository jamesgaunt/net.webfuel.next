using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class IsTeamMembersConsultedApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Querys
            
            app.MapPost("api/is-team-members-consulted/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<QueryResult<IsTeamMembersConsulted>> Query([FromBody] QueryIsTeamMembersConsulted command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

