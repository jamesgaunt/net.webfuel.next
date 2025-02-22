﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Common;
using Webfuel.Domain;
using Webfuel.Reporting;

namespace Webfuel.App
{
    [ApiService]
    public static class ReportGeneratorApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            app.MapPost("api/report-generator/{taskId:guid}", GenerateReport)
                .RequireIdentity();

            app.MapDelete("api/report-generator/{taskId:guid}", CancelReport)
                   .RequireIdentity();

            app.MapGet("download-report/{taskId:guid}", Download);
        }

        public static Task<ReportStep> GenerateReport(Guid taskId, IReportGeneratorService service)
        {
            return service.GenerateReport(taskId);
        }

        public static Task CancelReport(Guid taskId, IReportGeneratorService service)
        {
            return service.CancelReport(taskId);
        }

        public static object ExtractReportData(Guid taskId, IReportGeneratorService service)
        {
            return service.ExtractReportData(taskId);
        }

        [ApiIgnore]
        public static async Task<IResult> Download(Guid taskId, IReportGeneratorService service)
        {
            var result = await service.RenderReport(taskId);

            if (result?.MemoryStream != null)
            {
                return Results.File(
                    fileContents: result.MemoryStream.ToArray(),
                    contentType: result.ContentType,
                    fileDownloadName: result.Filename);
            }

            return Results.NoContent();
        }
    }
}
