using Microsoft.Extensions.DependencyInjection;
using Webfuel.Common;
using Webfuel.Domain.StaticData;
using Webfuel.Excel;
using Webfuel.Reporting;

namespace Webfuel.Domain;

internal class SupportHoursSpentReportArguments
{
    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }
}

internal class SupportHoursSpentReportData
{
    public List<DataLine> RawDataLines { get; set; } = new List<DataLine>();

    public List<DataLine> SummaryLines { get; set; } = new List<DataLine>();

    public Dictionary<Guid, Project?> ProjectCache { get; set; } = new Dictionary<Guid, Project?>();

    public class DataLine
    {
        public required DateOnly DateOfSupport { get; set; }

        public required string ProjectCode { get; set; }

        public required Guid SupportProvidedId { get; set; }

        public required decimal TimeInHours { get; set; }
    }
}

internal class SupportHoursSpentReportBuilder : ReportBuilder
{
    public SupportHoursSpentReportBuilder(ReportRequest request) : base(request)
    {
        Arguments.StartDate = request.Arguments.Single(p => p.Name == "Start Date").DateValue;
        Arguments.EndDate = request.Arguments.Single(p => p.Name == "End Date").DateValue;

        Query = new Query();
        Query.GreaterThanOrEqual(nameof(ProjectSupport.Date), Arguments.StartDate, Arguments.StartDate != null);
        Query.LessThanOrEqual(nameof(ProjectSupport.Date), Arguments.EndDate, Arguments.EndDate != null);
    }

    SupportHoursSpentReportArguments Arguments = new SupportHoursSpentReportArguments();

    SupportHoursSpentReportData CustomData = new SupportHoursSpentReportData();

    public override async Task InitialisationStep()
    {
        await base.InitialisationStep();
    }

    protected override async Task<IEnumerable<object>> QueryItems(int skip, int take)
    {
        Query.Skip = skip;
        Query.Take = take;
        var result = await ServiceProvider.GetRequiredService<IProjectSupportRepository>().QueryProjectSupport(Query, selectItems: true, countTotal: false);
        return result.Items;
    }

    protected override async Task<int> GetTotalCount()
    {
        var result = await ServiceProvider.GetRequiredService<IProjectSupportRepository>().QueryProjectSupport(Query, selectItems: false, countTotal: true);
        return result.TotalCount;
    }

    public override async ValueTask ProcessItem(object item)
    {
        var projectSupport = item as ProjectSupport;
        if (projectSupport == null)
            return;

        if (projectSupport.SupportProvidedIds.Count == 0 || projectSupport.WorkTimeInHours == 0)
            return; // No support provided so nothing to record

        var timeInHours = projectSupport.WorkTimeInHours * projectSupport.AdviserIds.Count() / projectSupport.SupportProvidedIds.Count();

        var project =
            CustomData.ProjectCache.ContainsKey(projectSupport.ProjectSupportGroupId) ?
            CustomData.ProjectCache[projectSupport.ProjectSupportGroupId] :
            CustomData.ProjectCache[projectSupport.ProjectSupportGroupId] = await ServiceProvider.GetRequiredService<IProjectRepository>().GetProjectByProjectSupportGroupId(projectSupport.ProjectSupportGroupId);

        if (project == null)
            return; // This support isn't on a project, so nothing to record    

        foreach (var supportProvidedId in projectSupport.SupportProvidedIds)
        {
            var rawDataLine = new SupportHoursSpentReportData.DataLine
            {
                DateOfSupport = projectSupport.Date,
                ProjectCode = project.PrefixedNumber,
                SupportProvidedId = supportProvidedId,
                TimeInHours = timeInHours
            };
            CustomData.RawDataLines.Add(rawDataLine);

            var summaryLine = CustomData.SummaryLines.FirstOrDefault(p => p.SupportProvidedId == supportProvidedId);
            if (summaryLine == null)
            {
                summaryLine = new SupportHoursSpentReportData.DataLine
                {
                    DateOfSupport = projectSupport.Date,
                    ProjectCode = String.Empty,
                    SupportProvidedId = supportProvidedId,
                    TimeInHours = 0
                };
                CustomData.SummaryLines.Add(summaryLine);
            }
            summaryLine.TimeInHours += timeInHours;
        }
    }

    public override async Task RenderStep()
    {
        if (Workbook == null)
        {
            var fileStorageService = ServiceProvider.GetRequiredService<IFileStorageService>();
            var memoryStream = await fileStorageService.LoadDirect("/_internal/report-templates/support_hours_spent.xlsx");

            Workbook = memoryStream == null ? new ExcelWorkbook() : new ExcelWorkbook(memoryStream);
            // Leave the stream open!

            var _worksheet = Workbook.GetOrCreateWorksheet("Raw Data");

            _worksheet.Cell(1, 01).SetValue("Date of Support").SetBold(true);
            _worksheet.Cell(1, 02).SetValue("Time in Hours").SetBold(true);
            _worksheet.Cell(1, 03).SetValue("Project").SetBold(true);
            _worksheet.Cell(1, 04).SetValue("Support Provided").SetBold(true);

            RowIndex = 2;
        }

        var worksheet = Workbook.GetWorksheet("Raw Data");

        var stepTimestamp = MicrosecondTimer.Timestamp;
        var stepItems = 0;
        do
        {
            var staticData = await ServiceProvider.GetRequiredService<IStaticDataService>().GetStaticData();

            if (StageCount >= CustomData.RawDataLines.Count)
            {
                await FinaliseReport();

                Result = new ReportResult
                {
                    MemoryStream = Workbook.ToMemoryStream(),
                    Filename = "report.xlsx",
                    ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                };

                Stage = ReportStage.Complete;
                Complete = true;
                break;
            }

            var rawDataLine = CustomData.RawDataLines[StageCount];

            var supportProvided = staticData.SupportProvided.FirstOrDefault(p => p.Id == rawDataLine.SupportProvidedId);

            worksheet.Cell(RowIndex, 01).SetValue(rawDataLine.DateOfSupport);
            worksheet.Cell(RowIndex, 02).SetValue(rawDataLine.TimeInHours).SetNumberFormat("#,##0.00");
            worksheet.Cell(RowIndex, 03).SetValue(rawDataLine.ProjectCode);
            worksheet.Cell(RowIndex, 04).SetValue(supportProvided?.Name ?? "Invalid Support Provided Type");

            RowIndex++;
            StageCount++;
            stepItems++;
        }
        while (MicrosecondTimer.Timestamp - stepTimestamp < MICROSECONDS_PER_STEP);

        Metrics.AddRender(MicrosecondTimer.Timestamp - stepTimestamp, stepItems);
    }

    async Task FinaliseReport()
    {
        var staticData = await ServiceProvider.GetRequiredService<IStaticDataService>().GetStaticData();
        var summaryRowCount = 0;

        {
            var chartSheet = Workbook!.GetOrCreateWorksheet("Chart");
            chartSheet.Cell("B3").SetValue(Arguments.StartDate);
            chartSheet.Cell("E3").SetValue(Arguments.EndDate);
        }

        {
            var summarySheet = Workbook!.GetOrCreateWorksheet("Summary");
            summarySheet.Cell(1, 01).SetValue("Support Provided").SetBold(true);
            summarySheet.Cell(1, 02).SetValue("Time in Hours").SetBold(true);

            var summaryLines = CustomData.SummaryLines.OrderBy(p => p.TimeInHours);

            foreach (var summaryLine in summaryLines)
            {
                var supportProvided = staticData.SupportProvided.FirstOrDefault(p => p.Id == summaryLine.SupportProvidedId);

                summarySheet.Cell(summaryRowCount + 2, 01).SetValue(supportProvided?.Name ?? "Invalid Support Provided Type");
                summarySheet.Cell(summaryRowCount + 2, 02).SetValue(summaryLine.TimeInHours).SetNumberFormat("#,##0.00");
                summaryRowCount++;
            }
            summarySheet.Column(01).AdjustToContents();
            summarySheet.Column(02).AdjustToContents();

            // Set data range for the chart
            Workbook!.SetNamedRange("CHART_NAMES", "Summary!$A$2:$A$" + (summaryLines.Count() + 1));
            Workbook!.SetNamedRange("CHART_VALUES", "Summary!$B$2:$B$" + (summaryLines.Count() + 1));
        }

        {
            var rawDataSheet = Workbook.GetOrCreateWorksheet("Raw Data");
            rawDataSheet.Column(01).AdjustToContents();
            rawDataSheet.Column(02).AdjustToContents();
            rawDataSheet.Column(03).AdjustToContents();
            rawDataSheet.Column(04).AdjustToContents();
        }
    }

    public static Task<List<ReportArgument>> GenerateArguments(ReportDesign design, IServiceProvider serviceProvider)
    {
        var result = new List<ReportArgument>();

        result.Add(new ReportArgument
        {
            Name = "Start Date",
            FilterId = Guid.NewGuid(),
            FieldId = Guid.NewGuid(),
            ReportProviderId = ReportProviderEnum.CustomReport,
            FieldType = ReportFieldType.Date,
            FilterType = ReportFilterType.Date,
        });

        result.Add(new ReportArgument
        {
            Name = "End Date",
            FilterId = Guid.NewGuid(),
            FieldId = Guid.NewGuid(),
            ReportProviderId = ReportProviderEnum.CustomReport,
            FieldType = ReportFieldType.Date,
            FilterType = ReportFilterType.Date,
        });

        return Task.FromResult(result);
    }
}
