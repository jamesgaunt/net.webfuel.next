using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class ProfessionalBackgroundDetailApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Commands
            
            app.MapPost("api/professional-background-detail", Create)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/professional-background-detail", Update)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/professional-background-detail/sort", Sort)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapDelete("api/professional-background-detail/{id:guid}", Delete)
                .RequireClaim(c => c.CanEditStaticData);
            
            // Querys
            
            app.MapPost("api/professional-background-detail/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<ProfessionalBackgroundDetail> Create([FromBody] CreateProfessionalBackgroundDetail command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task<ProfessionalBackgroundDetail> Update([FromBody] UpdateProfessionalBackgroundDetail command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Sort([FromBody] SortProfessionalBackgroundDetail command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Delete(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteProfessionalBackgroundDetail { Id = id });
        }
        
        public static Task<QueryResult<ProfessionalBackgroundDetail>> Query([FromBody] QueryProfessionalBackgroundDetail command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

