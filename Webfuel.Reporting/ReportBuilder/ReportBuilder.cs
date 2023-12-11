using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Extensions.DependencyInjection;
using Webfuel.Excel;

namespace Webfuel.Reporting
{

    public static class ReportStage
    {
        public const string Initialisation = "Initialisation";
        
        public const string Generating = "Generating";
        
        public const string Rendering = "Rendering";
        
        public const string Complete = "Complete";
    }

    /// <summary>
    /// Standard report builder that generates a report in Excel format using the specified report schema and design.
    /// </summary>
    public class ReportBuilder : ReportBuilderBase
    {
        const long MICROSECONDS_PER_LOAD = 1000 * 25;

        const long MICROSECONDS_PER_STEP = 1000 * 100;

        protected int ItemsPerLoad { get; set; } = 10; // Initial value, will auto-tune

        public ReportBuilder(ReportRequest request)
        {
            Request = request;
            Stage = ReportStage.Initialisation;
        }

        public ReportRequest Request { get; }

        public ReportData Data { get; } = new ReportData();
        
        public ExcelWorkbook? Workbook { get; set; }
        
        public ReportResult? Result { get; set; }

        public ReportSchema Schema
        {
            get
            {
                if(_schema == null) { 
                    _schema = ServiceProvider.GetRequiredService<IReportDesignService>()
                        .GetReportSchema(Request.ReportProviderId);
                }
                return _schema;
            }
        }
        ReportSchema? _schema = null;

        public async Task<IEnumerable<object>> QueryItems(int skip, int take)
        {
            return await ServiceProvider.GetRequiredService<IReportDesignService>().QueryItems(Request.ReportProviderId, StageCount, ItemsPerLoad);
        }

        public async Task<int> GetTotalCount()
        {
            return await ServiceProvider.GetRequiredService<IReportDesignService>().GetTotalCount(Request.ReportProviderId);
        }

        public override async Task GenerateReport()
        {
            if (Stage == ReportStage.Initialisation)
                await InitialisationStep();
            else if (Stage == ReportStage.Generating)
                await GenerationStep();
            else if (Stage == ReportStage.Rendering)
                await RenderStep();
            else
                throw new InvalidOperationException($"Unknown stage: {Stage}");
        }

        public virtual async Task InitialisationStep()
        {
            StageTotal = await GetTotalCount();
            StageCount = 0;
            Stage = ReportStage.Generating;
        }

        public void TuneItemsPerLoad(long microseconds)
        {
            long microsecondsPerItem = microseconds / ItemsPerLoad;

            ItemsPerLoad = (int)(MICROSECONDS_PER_LOAD / microsecondsPerItem);

            if (ItemsPerLoad < 1)
                ItemsPerLoad = 1;
            if (ItemsPerLoad > 100)
                ItemsPerLoad = 100;
        }

        public virtual async Task GenerationStep()
        {
            var stepTimestamp = MicrosecondTimer.Timestamp;
            var stepItems = 0;
            do
            {
                var loadTimestamp = MicrosecondTimer.Timestamp;
                var items = await QueryItems(StageCount, ItemsPerLoad);
                var loadMicroseconds = MicrosecondTimer.Timestamp - loadTimestamp;

                var none = true;
                var loadItems = 0;
                foreach (var item in items)
                {
                    await ProcessItem(item);
                    none = false;
                    stepItems++;
                    loadItems++;
                }

                if (none)
                {
                    StageTotal = Data.Rows.Count;
                    StageCount = 0;
                    Stage = ReportStage.Rendering;
                    break;
                }

                StageCount += loadItems;
                Metrics.AddLoad(loadMicroseconds, loadItems);
                TuneItemsPerLoad(loadMicroseconds);
            }
            while (MicrosecondTimer.Timestamp - stepTimestamp < MICROSECONDS_PER_STEP);

            Metrics.AddGeneration(MicrosecondTimer.Timestamp - stepTimestamp, stepItems);
        }

        public virtual Task RenderStep()
        {
            if (Workbook == null)
            {
                Workbook = new ExcelWorkbook();
                var _worksheet = Workbook.GetOrCreateWorksheet();

                // Render Headers
                for (var i = 0; i < Request.Design.Columns.Count; i++)
                    _worksheet.Cell(1, i + 1).SetValue(Request.Design.Columns[i].Title).SetBold(true);
            }

            var worksheet = Workbook.GetOrCreateWorksheet();

            var stepTimestamp = MicrosecondTimer.Timestamp;
            var stepItems = 0;
            do
            {
                if (StageCount >= Data.Rows.Count)
                {
                    GenerateResult();
                    Stage = ReportStage.Complete;
                    Complete = true;
                    break;
                }

                var row = Data.Rows[StageCount];
                for (var c = 0; c < row.Cells.Count; c++)
                {
                    worksheet.Cell(StageCount + 2, c + 1).SetValue(row.Cells[c].Value);
                }

                StageCount++;
                stepItems++;
            }
            while (MicrosecondTimer.Timestamp - stepTimestamp < MICROSECONDS_PER_STEP);

            Metrics.AddRender(MicrosecondTimer.Timestamp - stepTimestamp, stepItems);
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

        public virtual async Task<bool> FilterItem(object item)
        {
            foreach (var filter in Request.Design.Filters)
            {
                var result = await filter.Apply(item, this);
                if (result == false)
                    return false;
            }
            return true;
        }

        public virtual async Task ProcessItem(object item)
        {
            if (!await FilterItem(item))
                return;

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
            var field = ServiceProvider.GetRequiredService<IReportDesignService>()
                .GetReportSchema(Request.ReportProviderId)
                .GetField(column.FieldId);

            return field.Evaluate(item, this);
        }

        public override ReportResult RenderReport()
        {
            return Result ?? throw new InvalidOperationException("Report has not been generated yet");
        }
    }
}
