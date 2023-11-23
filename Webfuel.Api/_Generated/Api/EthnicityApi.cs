using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class EthnicityApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Commands
            
            app.MapPost("api/ethnicity", Create)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/ethnicity", Update)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/ethnicity/sort", Sort)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapDelete("api/ethnicity/{id:guid}", Delete)
                .RequireClaim(c => c.CanEditStaticData);
            
            // Querys
            
            app.MapPost("api/ethnicity/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<Ethnicity> Create([FromBody] CreateEthnicity command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task<Ethnicity> Update([FromBody] UpdateEthnicity command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Sort([FromBody] SortEthnicity command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Delete(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteEthnicity { Id = id });
        }
        
        public static Task<QueryResult<Ethnicity>> Query([FromBody] QueryEthnicity command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

