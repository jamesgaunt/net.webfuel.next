using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class WillStudyUseCTUApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Querys
            
            app.MapPost("api/will-study-use-ctu/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<QueryResult<WillStudyUseCTU>> Query([FromBody] QueryWillStudyUseCTU command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

