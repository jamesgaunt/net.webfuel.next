using Webfuel.Excel;

namespace Webfuel.Reporting
{
    public abstract class SchemaReportBuilder : ReportBuilder
    {
        const long MICROSECONDS_PER_STEP = 1000 * 100;
        protected int ItemsPerStep { get; set; } = 10;

        public SchemaReportBuilder(ReportSchema schema, ReportRequest request, ReportQuery query)
        {
            Schema = schema;
            Request = request;
            Query = query;
        }

        public override async Task InitialiseReport()
        {
            TotalCount = await Query.GetTotalCount(ServiceProvider);
        }

        public override async Task GenerateReport()
        {
            var startTimestamp = MicrosecondTimer.Timestamp;
            do
            {
                var items = await Query.GetItems(ProgressCount, ItemsPerStep, ServiceProvider);

                if (!items.Any())
                {
                    ProgressCount = TotalCount;
                    Complete = true;
                    return;
                }

                foreach (var item in items)
                    await ProcessItem(item);

                ProgressCount += ItemsPerStep;
            }
            while (MicrosecondTimer.Timestamp - startTimestamp < MICROSECONDS_PER_STEP);
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

        public virtual async Task<object?> EvaluateColumn(object item, ReportColumn column)
        {
            var field = Schema.Fields.FirstOrDefault(p => p.Id == column.FieldId);
            
            if (field == null)
                return null;

            if (field.Accessor != null)
                return field.Accessor(item);

            if(field.AsyncAccessor != null)
                return await field.AsyncAccessor(item);

            // Scribble Expressions, Reflection?

            return null;
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
                worksheet.Cell(1, i + 1).SetValue(Request.Design.Columns[i].Title);
            }

            var ri = 2;
            foreach(var row in Sheet.Rows)
            {
                var ci = 1;
                foreach(var cell in row.Cells)
                {
                    worksheet.Cell(ri, ci).SetValue(cell.Value);
                    ci++;
                }
                ri++;
            }

            worksheet.AutoFitColumns();

            return workbook;
        }

        public ReportSheet Sheet { get; } = new ReportSheet();
        public ReportRequest Request { get; }
        public ReportSchema Schema { get; }
        public ReportQuery Query { get; }
    }
}
