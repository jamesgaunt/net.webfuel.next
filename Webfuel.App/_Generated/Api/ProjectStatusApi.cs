using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class ProjectStatusApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Querys
            
            app.MapPost("api/project-status/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<QueryResult<ProjectStatus>> Query([FromBody] QueryProjectStatus command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

