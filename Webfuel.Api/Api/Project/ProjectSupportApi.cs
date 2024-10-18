using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Webfuel.Domain;

namespace Webfuel.App
{
    public class UploadProjectSupportFileDTO
    {
        public required Guid ProjectId { get; set; }
    }

    [ApiService]
    [ApiDataSource]
    public static class ProjectSupportApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            // Commands

            app.MapPost("api/project-support", Create)
                .RequireIdentity();

            app.MapPut("api/project-support", Update)
                .RequireIdentity();

            app.MapPut("api/project-support/resend-notification", ResendNotification)
                .RequireIdentity();

            app.MapPut("api/project-support/completion", UpdateCompletion)
                .RequireIdentity();

            app.MapPut("api/project-support/complete", Complete)
                 .RequireIdentity();

            app.MapPut("api/project-support/uncomplete", Uncomplete)
                     .RequireIdentity();

            app.MapDelete("api/project-support/{id:guid}", Delete)
                .RequireIdentity();

            // Querys

            app.MapPost("api/project-support/query", Query)
                .RequireIdentity();

            app.MapGet("api/project-support/{id:guid}", Get)
                .RequireIdentity();

            // Files

            app.MapPost("api/project-support/select-file", SelectFile)
                 .RequireIdentity();

            app.MapPost("api/project-support/upload-file", UploadFile)
                .RequireIdentity();
        }

        public static Task<ProjectSupport> Create([FromBody] CreateProjectSupport command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<ProjectSupport> Update([FromBody] UpdateProjectSupport command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<ProjectSupport> ResendNotification([FromBody] ResendProjectSupportNotification command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<ProjectSupport> UpdateCompletion([FromBody] UpdateProjectSupportCompletion command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<ProjectSupport> Complete([FromBody] CompleteProjectSupport command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<ProjectSupport> Uncomplete([FromBody] UncompleteProjectSupport command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task Delete(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteProjectSupport { Id = id });
        }

        public static Task<QueryResult<ProjectSupport>> Query([FromBody] QueryProjectSupport command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static async Task<ProjectSupport?> Get(Guid id, IMediator mediator)
        {
            return await mediator.Send(new GetProjectSupport { Id = id });
        }

        public static async Task<List<ProjectSupportFile>> SelectFile([FromBody] SelectProjectSupportFile command, IMediator mediator)
        {
            return await mediator.Send(command);
        }

        public static async Task<List<ProjectSupportFile>> UploadFile(HttpRequest request, IMediator mediator)
        {
            var data = request.Form["data"].ToString();
            if (String.IsNullOrEmpty(data))
                throw new InvalidOperationException("No data part found on upload project support file request");

            var command = JsonSerializer.Deserialize<UploadProjectSupportFileDTO>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (command == null)
                throw new InvalidOperationException("Unable to deserialize upload project support file command");

            var result = new List<ProjectSupportFile>();
            foreach (var file in request.Form.Files)
                result.Add(await mediator.Send(new UploadProjectSupportFile { ProjectId = command.ProjectId, FormFile = file }));

            return result;
        }
    }
}
