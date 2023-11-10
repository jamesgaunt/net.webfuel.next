using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Webfuel.Common;
using Webfuel.Domain;
using static Webfuel.App.PingApi;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    public static class FileStorageEntryApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            app.MapPost("api/file-storage-entry/query", Query)
                 .RequireIdentity();

            app.MapPost("api/file-storage-entry/upload", Upload)
                .RequireIdentity();

            app.MapDelete("api/file-storage-entry/{id:guid}", Delete)
                .RequireIdentity();

            // Api Ignore

            app.MapGet("api/file-storage-entry/sas-redirect/{fileStorageEntryId:guid}", SasRedirect); // TODO: Time limited link
        }

        public static async Task<QueryResult<FileStorageEntry>> Query([FromBody]QueryFileStorageEntry query, IFileStorageService fileStorageService)
        {
            return await fileStorageService.QueryFiles(query);
        }

        public static async Task Upload(HttpRequest request, IFileStorageService fileStorageService)
        {
            var data = request.Form["data"].ToString();
            if (String.IsNullOrEmpty(data))
                throw new InvalidOperationException("No data part found on upload file request");

            var command = JsonSerializer.Deserialize<UploadFileStorageEntry>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (command == null)
                throw new InvalidOperationException("Unable to deserialize upload file command");

            foreach (var file in request.Form.Files)
                await fileStorageService.UploadFile(new UploadFileStorageEntry { FileStorageGroupId = command.FileStorageGroupId, FormFile = file });
        }

        public static async Task Delete(Guid id, IFileStorageService fileStorageService)
        {
            await fileStorageService.DeleteFile(id);
        }

        // Api Ignore

        [ApiIgnore]
        public static async Task SasRedirect(Guid fileStorageEntryId, HttpResponse response, IFileStorageService fileStorageService)
        {
            var sasUri = await fileStorageService.GenerateFileSasUri(fileStorageEntryId);

            response.Redirect(sasUri, false);
            await response.CompleteAsync();
        }
    }
}
