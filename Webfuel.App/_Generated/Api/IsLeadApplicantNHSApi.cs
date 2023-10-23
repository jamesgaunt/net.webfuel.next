using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class IsLeadApplicantNHSApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Querys
            
            app.MapPost("api/is-lead-applicant-nhs/query", Query)
                .RequireIdentity();
        }
        
        public static Task<QueryResult<IsLeadApplicantNHS>> Query([FromBody] QueryIsLeadApplicantNHS command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

