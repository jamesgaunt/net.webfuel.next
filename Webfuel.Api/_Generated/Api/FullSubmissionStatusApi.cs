using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class FullSubmissionStatusApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Querys
            
            app.MapPost("api/full-submission-status/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<QueryResult<FullSubmissionStatus>> Query([FromBody] QueryFullSubmissionStatus command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

