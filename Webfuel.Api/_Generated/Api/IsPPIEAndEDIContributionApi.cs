using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class IsPPIEAndEDIContributionApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Querys
            
            app.MapPost("api/is-ppie-and-edi-contribution/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<QueryResult<IsPPIEAndEDIContribution>> Query([FromBody] QueryIsPPIEAndEDIContribution command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

