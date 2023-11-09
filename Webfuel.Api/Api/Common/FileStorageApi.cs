using MediatR;
using Webfuel.Common;
using Webfuel.Domain;
using static Webfuel.App.PingApi;

namespace Webfuel.App
{
    [ApiService]
    public static class FileStorageApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            app.MapGet("api/file-storage/files/{fileStorageGroupId:guid}", ListFiles)
                 .RequireIdentity();

            app.MapDelete("api/file-storage/files/{fileStorageEntryId:guid}", ListFiles)
                .RequireIdentity();
        }

        public static async Task<List<FileStorageEntry>> ListFiles(Guid fileStorageGroupId, IFileStorageService fileStorageService)
        {
            return await fileStorageService.ListFiles(fileStorageGroupId);
        }

        public static async Task DeleteFile(Guid fileStorageEntryId, IFileStorageService fileStorageService)
        {
            await fileStorageService.DeleteFile(fileStorageEntryId);
        }
    }
}
