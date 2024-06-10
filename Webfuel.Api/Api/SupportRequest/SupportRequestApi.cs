using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using Webfuel.Common;
using Webfuel.Domain;
using Webfuel.Reporting;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    public static class SupportRequestApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            // External

            app.MapPost("api/support-request/files", SubmitFiles);

            app.MapPost("api/support-request/form", SubmitForm);

            // Commands

            app.MapPut("api/support-request", Update)
                .RequireClaim(c => c.CanTriageSupportRequests);

            app.MapPut("api/support-request/researcher", UpdateResearcher)
                .RequireClaim(c => c.CanTriageSupportRequests);

            app.MapPut("api/support-request/triage", Triage)
                .RequireClaim(c => c.CanTriageSupportRequests);

            app.MapPut("api/support-request/unlock", Unlock)
                .RequireClaim(c => c.CanTriageSupportRequests);

            app.MapDelete("api/support-request/{id:guid}", Delete)
                .RequireClaim(c => c.CanTriageSupportRequests);

            // Querys

            app.MapPost("api/support-request/query", Query)
                .RequireIdentity();

            app.MapGet("api/support-request/{id:guid}", Get)
                .RequireIdentity();

            app.MapPut("api/support-request/export", Export)
                     .RequireIdentity();
        }

        public static async Task<SupportRequest> SubmitFiles(
            HttpRequest request,
            IFileStorageService fileStorageService,
            IErrorLogService errorLogService)
        {
            try
            {
                var fileStorageGroup = await fileStorageService.CreateGroup();

                foreach (var file in request.Form.Files)
                    await fileStorageService.UploadFile(new UploadFileStorageEntry { FileStorageGroupId = fileStorageGroup.Id, FormFile = file });

                return new SupportRequest { FileStorageGroupId = fileStorageGroup.Id };
            }
            catch (Exception ex)
            {
                await errorLogService.LogException("Exception submitting support request files", ex: ex);
                throw;
            }
        }

        public static async Task<SupportRequest> SubmitForm(
            [FromBody] CreateSupportRequest command,
            IMediator mediator,
            IErrorLogService errorLogService)
        {
            try
            {
                return await mediator.Send(command);
            }
            catch (Exception ex)
            {
                await errorLogService.LogException("Exception submitting support request", ex: ex);
                throw;
            }
        }

        public static Task<SupportRequest> Update([FromBody] UpdateSupportRequest command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<SupportRequest> UpdateResearcher([FromBody] UpdateSupportRequestResearcher command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<SupportRequest> Triage([FromBody] TriageSupportRequest command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<SupportRequest> Unlock([FromBody] UnlockSupportRequest command, IMediator mediator)
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

        public static Task<ReportStep> Export([FromBody] QuerySupportRequest request, IExportSupportRequestService service)
        {
            return service.InitialiseReport(request);
        }
    }
}
