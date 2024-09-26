using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class ProfessionalBackgroundApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Querys
            
            app.MapPost("api/professional-background/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<QueryResult<ProfessionalBackground>> Query([FromBody] QueryProfessionalBackground command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

