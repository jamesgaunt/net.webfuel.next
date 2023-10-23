using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class IsInternationalMultiSiteStudyApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Querys
            
            app.MapPost("api/is-international-multi-site-study/query", Query)
                .RequireIdentity();
        }
        
        public static Task<QueryResult<IsInternationalMultiSiteStudy>> Query([FromBody] QueryIsInternationalMultiSiteStudy command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

