using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class IsPrePostAwardApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Querys
            
            app.MapPost("api/is-pre-post-award/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<QueryResult<IsPrePostAward>> Query([FromBody] QueryIsPrePostAward command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

