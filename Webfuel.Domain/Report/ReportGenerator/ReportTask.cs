using Webfuel.Excel;

namespace Webfuel.Domain
{
    public class ReportTask : IDisposable
    {
        public Guid TaskId { get; internal set; }
        public required Type ReportGenerator { get; init; }

        public int ProgressCount { get; set; }
        public int TotalCount { get; set; }
        public bool Complete { get; set; }

        internal Guid? IdentityId { get; set; }
        internal DateTimeOffset CreatedAt { get; } = DateTimeOffset.UtcNow;
        internal DateTimeOffset LastAccessedAt { get; set; } = DateTimeOffset.UtcNow;

        public ReportDesign Design { get; set; } = new ReportDesign();
        public Query Query { get; set; } = new Query();
        public int CurrentRow { get; set; } = 1;
        public ExcelWorkbook Workbook { get; } = new ExcelWorkbook();

        public ExcelWorksheet Worksheet
        {
            get
            {
                return Workbook.GetOrCreateWorksheet(Design.WorksheetName);
            }
        }

        public ReportResult GenerateResult()
        {
            return new ReportResult
            {
                MemoryStream = Workbook.ToMemoryStream(),
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                FileDownloadName = FileName
            };
        }

        public void Dispose()
        {
            Workbook.Dispose();
        }

        string FileName
        {
            get
            {
                var filename = Design.FileName.Trim();
                if (String.IsNullOrEmpty(filename))
                    filename = "report.xlsx";
                else if (!filename.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
                    filename += ".xlsx";
                return filename;
            }
        }
    }
}
