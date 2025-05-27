using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class FullOutcomeApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Querys
            
            app.MapPost("api/full-outcome/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<QueryResult<FullOutcome>> Query([FromBody] QueryFullOutcome command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

