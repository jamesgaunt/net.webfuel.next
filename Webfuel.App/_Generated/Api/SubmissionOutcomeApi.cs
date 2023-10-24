using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class SubmissionOutcomeApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Querys
            
            app.MapPost("api/submission-outcome/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<QueryResult<SubmissionOutcome>> Query([FromBody] QuerySubmissionOutcome command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

