using Microsoft.Extensions.DependencyInjection;
using Webfuel.Common;
using Webfuel.Excel;

namespace Webfuel.Domain
{
    public class ProjectExportTask: ReportTask
    {
        public ProjectExportTask(ProjectExportRequest request)
        {
            Request = request;
            Query = Request.ToQuery();
            Workbook = new ExcelWorkbook();
            Worksheet = Workbook.AddWorksheet("ProjectExport");
        } 

        public override Type ReportGeneratorType => typeof(IProjectExportService);

        public override ReportResult GenerateResult()
        {
            return new ReportResult
            {
                MemoryStream = Workbook.ToMemoryStream(),
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                FileDownloadName = "project-export.xlsx"
            };
        }

        public override void Dispose()
        {
            Workbook.Dispose();
        }

        // Querying
        public ProjectExportRequest Request { get; }
        public QueryProject Query { get; }

        // Rendering
        public int Row { get; set; } = 1;
        public ExcelWorkbook Workbook { get; }

        public ExcelWorksheet Worksheet { get; }
    }
}
