using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class OutlineSubmissionStatusApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Querys
            
            app.MapPost("api/outline-submission-status/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<QueryResult<OutlineSubmissionStatus>> Query([FromBody] QueryOutlineSubmissionStatus command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

