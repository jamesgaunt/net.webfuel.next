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
            
            // Commands
            
            app.MapPost("api/professional-background", Create)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/professional-background", Update)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/professional-background/sort", Sort)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapDelete("api/professional-background/{id:guid}", Delete)
                .RequireClaim(c => c.CanEditStaticData);
            
            // Querys
            
            app.MapPost("api/professional-background/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<ProfessionalBackground> Create([FromBody] CreateProfessionalBackground command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task<ProfessionalBackground> Update([FromBody] UpdateProfessionalBackground command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Sort([FromBody] SortProfessionalBackground command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Delete(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteProfessionalBackground { Id = id });
        }
        
        public static Task<QueryResult<ProfessionalBackground>> Query([FromBody] QueryProfessionalBackground command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

