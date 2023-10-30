using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class IsFellowshipApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Querys
            
            app.MapPost("api/is-fellowship/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<QueryResult<IsFellowship>> Query([FromBody] QueryIsFellowship command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

