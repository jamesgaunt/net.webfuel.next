using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class SubmissionStatusApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Querys
            
            app.MapPost("api/submission-status/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<QueryResult<SubmissionStatus>> Query([FromBody] QuerySubmissionStatus command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

