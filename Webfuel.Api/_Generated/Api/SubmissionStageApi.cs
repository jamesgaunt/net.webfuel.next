using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class SubmissionStageApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Querys
            
            app.MapPost("api/submission-stage/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<QueryResult<SubmissionStage>> Query([FromBody] QuerySubmissionStage command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

