using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using Webfuel.Common;
using Webfuel.Domain;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    public static class SupportRequestApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            // Commands

            app.MapPost("api/support-request", Submit); // Called from external support request form, also takes in files
                

            app.MapPut("api/support-request", Update)
                .RequireIdentity();

            app.MapPut("api/support-request/researcher", UpdateResearcher)
                .RequireIdentity();

            app.MapPost("api/support-request/status", UpdateStatus)
                .RequireIdentity();

            app.MapDelete("api/support-request/{id:guid}", Delete)
                .RequireIdentity();

            // Querys

            app.MapPost("api/support-request/query", Query)
                .RequireIdentity();

            app.MapGet("api/support-request/{id:guid}", Get)
                .RequireIdentity();
        }

        public static async Task<SupportRequest> Submit(HttpRequest request, IMediator mediator, IFileStorageService fileStorageService)
        {
            var fileStorageGroup = await fileStorageService.CreateGroup();
            
            foreach(var file in request.Form.Files)
                await fileStorageService.UploadFile(new UploadFileStorageEntry { FileStorageGroupId = fileStorageGroup.Id, FormFile = file });

            var data = request.Form["data"].ToString();
            if (String.IsNullOrEmpty(data))
                throw new InvalidOperationException("No data part found on create support request http request");

            var command = JsonSerializer.Deserialize<CreateSupportRequest>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (command == null)
                throw new InvalidOperationException("Unable to deserialize create support request command");

            command.FileStorageGroupId = fileStorageGroup.Id;
            return await mediator.Send(command);
        }

        public static Task<SupportRequest> Update([FromBody] UpdateSupportRequest command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<SupportRequest> UpdateResearcher([FromBody] UpdateSupportRequestResearcher command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<SupportRequest> UpdateStatus([FromBody] UpdateSupportRequestStatus command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task Delete(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteSupportRequest { Id = id });
        }

        public static Task<QueryResult<SupportRequest>> Query([FromBody] QuerySupportRequest command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static async Task<SupportRequest?> Get(Guid id, IMediator mediator)
        {
            return await mediator.Send(new GetSupportRequest { Id = id });
        }
    }
}
