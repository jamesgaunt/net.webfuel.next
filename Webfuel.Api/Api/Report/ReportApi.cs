﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Common;
using Webfuel.Domain;

namespace Webfuel.App
{
    [ApiService]
    public static class ReportApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            app.MapPost("api/report/{taskId:guid}", GenerateReport)
                .RequireIdentity();

            app.MapDelete("api/report/cancel/{taskId:guid}", CancelReport)
                   .RequireIdentity();

            app.MapGet("download-report/{taskId:guid}", GenerateResult);
        }

        public static Task<ReportProgress> GenerateReport(Guid taskId, IReportService service)
        {
            return service.GenerateReport(taskId);
        }

        public static Task CancelReport(Guid taskId, IReportService service)
        {
            return service.CancelReport(taskId);
        }

        [ApiIgnore]
        public static async Task<IResult> GenerateResult(Guid taskId, IReportService service)
        {
            var result = await service.GenerateResult(taskId);

            if (result?.MemoryStream != null)
            {
                return Results.File(
                    fileContents: result.MemoryStream.ToArray(),
                    contentType: result.ContentType,
                    fileDownloadName: result.FileDownloadName);
            }

            return Results.NoContent();
        }
    }
}