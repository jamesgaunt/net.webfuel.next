using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class IsCTUAlreadyInvolvedApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Querys
            
            app.MapPost("api/is-ctu-already-involved/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<QueryResult<IsCTUAlreadyInvolved>> Query([FromBody] QueryIsCTUAlreadyInvolved command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

