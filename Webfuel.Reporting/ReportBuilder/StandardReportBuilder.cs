using Webfuel.Excel;

namespace Webfuel.Reporting
{
    /// <summary>
    /// Standard report builder that generates a report in Excel format using the specified report schema and design.
    /// </summary>
    public class StandardReportBuilder : ReportBuilder
    {
        const long MICROSECONDS_PER_STEP = 1000 * 100;
        protected int ItemsPerStep { get; set; } = 10;

        public StandardReportBuilder(ReportRequest request)
        {
            Request = request;
        }

        public ReportSheet Sheet { get; } = new ReportSheet();
        public ReportRequest Request { get; }

        public override async Task GenerateReport()
        {
            if (Stage == String.Empty)
            {
                Stage = "Generating";
                StageTotal = await ReportDesignService.GetTotalCount(Request.ReportProviderId);
                StageCount = 0;
            }

            else if (Stage == "Generating")
            {
                var startTimestamp = MicrosecondTimer.Timestamp;
                do
                {
                    var items = await ReportDesignService.QueryItems(Request.ReportProviderId, StageCount, ItemsPerStep);

                    var none = true;
                    foreach (var item in items)
                    {
                        await ProcessItem(item);
                        none = false;
                    }

                    if (none)
                    {
                        StageCount = StageTotal;
                        Complete = true;
                        return;
                    }

                    StageCount += ItemsPerStep;
                }
                while (MicrosecondTimer.Timestamp - startTimestamp < MICROSECONDS_PER_STEP);
            }
        }

        public virtual async Task ProcessItem(object item)
        {
            var row = Sheet.AddRow();
            foreach (var column in Request.Design.Columns)
            {
                await ProcessColumn(item, column, row);
            }
        }

        public virtual async Task ProcessColumn(object item, ReportColumn column, ReportRow row)
        {
            var value = await EvaluateColumn(item, column);
            row.AddCell().Value = value;
        }

        public virtual Task<object?> EvaluateColumn(object item, ReportColumn column)
        {
            var field = ReportDesignService.GetReportSchema(Request.ReportProviderId).Fields.FirstOrDefault(p => p.Id == column.FieldId);

            if (field == null)
                return Task.FromResult<object?>(null);

            return field.Evaluate(item, ServiceProvider);
        }

        public override Task<ReportResult> RenderReport()
        {
            var workbook = GenerateWorkbook();

            return Task.FromResult(new ReportResult
            {
                MemoryStream = workbook.ToMemoryStream(),
                Filename = "report.xlsx",
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            });
        }

        public virtual ExcelWorkbook GenerateWorkbook()
        {
            var workbook = new ExcelWorkbook();
            var worksheet = workbook.GetOrCreateWorksheet();

            for (var i = 0; i < Request.Design.Columns.Count; i++)
            {
                worksheet.Cell(1, i + 1).SetValue(Request.Design.Columns[i].Title).SetBold(true);
            }

            var ri = 2;
            foreach (var row in Sheet.Rows)
            {
                var ci = 1;
                foreach (var cell in row.Cells)
                {
                    worksheet.Cell(ri, ci).SetValue(cell.Value);
                    ci++;
                }
                ri++;
            }

            for (var i = 0; i < Request.Design.Columns.Count; i++)
            {
                var width = Request.Design.Columns[i].Width;
                if (width == null)
                    worksheet.Column(i + 1).AdjustToContents();
                else
                    worksheet.Column(i + 1).SetWidth(width.Value);
            }

            return workbook;
        }
    }
}
