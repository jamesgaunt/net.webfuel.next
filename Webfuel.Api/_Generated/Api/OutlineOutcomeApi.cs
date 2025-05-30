using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class OutlineOutcomeApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Querys
            
            app.MapPost("api/outline-outcome/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<QueryResult<OutlineOutcome>> Query([FromBody] QueryOutlineOutcome command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

