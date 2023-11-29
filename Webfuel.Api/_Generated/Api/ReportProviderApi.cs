using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class ReportProviderApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Querys
            
            app.MapPost("api/report-provider/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<QueryResult<ReportProvider>> Query([FromBody] QueryReportProvider command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

