using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class FileTagApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Commands
            
            app.MapPost("api/file-tag", Create)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/file-tag", Update)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/file-tag/sort", Sort)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapDelete("api/file-tag/{id:guid}", Delete)
                .RequireClaim(c => c.CanEditStaticData);
            
            // Querys
            
            app.MapPost("api/file-tag/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<FileTag> Create([FromBody] CreateFileTag command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task<FileTag> Update([FromBody] UpdateFileTag command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Sort([FromBody] SortFileTag command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Delete(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteFileTag { Id = id });
        }
        
        public static Task<QueryResult<FileTag>> Query([FromBody] QueryFileTag command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

