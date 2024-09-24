using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class IsYesNoApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Querys
            
            app.MapPost("api/is-yes-no/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<QueryResult<IsYesNo>> Query([FromBody] QueryIsYesNo command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

