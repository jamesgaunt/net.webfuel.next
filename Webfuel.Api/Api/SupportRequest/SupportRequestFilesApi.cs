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
    public static class SupportRequestFilesApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            // Querys

            app.MapPost("api/support-request/list-files", ListFiles)
                .RequireIdentity();
        }

        public static Task<List<FileStorageEntry>> ListFiles([FromBody] ListSupportRequestFiles command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}
