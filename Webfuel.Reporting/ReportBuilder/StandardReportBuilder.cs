using DocumentFormat.OpenXml.Spreadsheet;
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

        public ReportRequest Request { get; }
        public ReportData Data { get; } = new ReportData();
        public ExcelWorkbook? Workbook { get; set; }
        public ReportResult? Result { get; set; }

        public override async Task GenerateReport()
        {
            if (Stage == String.Empty)
                await InitialisationStep();
            else if (Stage == "Generating")
                await GenerationStep();
            else if (Stage == "Rendering")
                await RenderStep();
            else 
                throw new InvalidOperationException($"Unknown stage: {Stage}");
        }

        public virtual async Task InitialisationStep()
        {
            StageTotal = await ReportDesignService.GetTotalCount(Request.ReportProviderId);
            StageCount = 0;
            Stage = "Generating";
        }

        public virtual async Task GenerationStep()
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
                    StageTotal = Data.Rows.Count;
                    StageCount = 0;
                    Stage = "Rendering";
                    return;
                }

                StageCount += ItemsPerStep;
            }
            while (MicrosecondTimer.Timestamp - startTimestamp < MICROSECONDS_PER_STEP);
        }

        public virtual Task RenderStep()
        {
            if(Workbook == null)
            {
                Workbook = new ExcelWorkbook();
                var _worksheet = Workbook.GetOrCreateWorksheet();

                // Render Headers
                for (var i = 0; i < Request.Design.Columns.Count; i++)
                    _worksheet.Cell(1, i + 1).SetValue(Request.Design.Columns[i].Title).SetBold(true);
            }

            var worksheet = Workbook.GetOrCreateWorksheet();
            var startTimestamp = MicrosecondTimer.Timestamp;
            do
            {
                if(StageCount >= Data.Rows.Count)
                {
                    GenerateResult();
                    Complete = true;
                    return Task.CompletedTask;
                }

                var row = Data.Rows[StageCount];
                for(var c = 0; c < row.Cells.Count; c++)
                {
                    worksheet.Cell(StageCount + 2, c + 1).SetValue(row.Cells[c].Value);
                }

                StageCount++;
            }
            while (MicrosecondTimer.Timestamp - startTimestamp < MICROSECONDS_PER_STEP);

            return Task.CompletedTask;
        }

        public virtual void GenerateResult()
        {
            if (Workbook == null)
                throw new InvalidOperationException("Workbook has not been generated yet");

            FormatWorksheet(Workbook.GetOrCreateWorksheet());
            Result = new ReportResult
            {
                MemoryStream = Workbook.ToMemoryStream(),
                Filename = "report.xlsx",
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            };
        }

        public virtual void FormatWorksheet(ExcelWorksheet worksheet)
        {
            for (var i = 0; i < Request.Design.Columns.Count; i++)
            {
                var width = Request.Design.Columns[i].Width;
                if (width == null)
                    worksheet.Column(i + 1).AdjustToContents();
                else
                    worksheet.Column(i + 1).SetWidth(width.Value);
            }
        }

        public virtual async Task ProcessItem(object item)
        {
            var row = Data.AddRow();
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

        public override ReportResult RenderReport()
        {
            return Result ?? throw new InvalidOperationException("Report has not been generated yet");
        }
    }
}
