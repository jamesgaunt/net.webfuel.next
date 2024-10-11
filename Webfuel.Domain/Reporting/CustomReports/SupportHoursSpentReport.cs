using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Webfuel.Domain.StaticData;
using Webfuel.Excel;
using Webfuel.Reporting;

namespace Webfuel.Domain
{
    internal class SupportHoursSpentReportArguments
    {
        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }
    }

    internal class SupportHoursSpentReportData
    {
        public List<RawDataLine> RawDataLines { get; set; } = new List<RawDataLine>();

        public Dictionary<Guid, Project> ProjectCache { get; set; } = new Dictionary<Guid, Project>();

        public class RawDataLine
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

            if (projectSupport.SupportProvidedIds.Count == 0)
                return; // No support provided so nothing to record

            var timeInHours = projectSupport.WorkTimeInHours * projectSupport.AdviserIds.Count() / projectSupport.SupportProvidedIds.Count();

            var project = CustomData.ProjectCache.ContainsKey(projectSupport.ProjectId) ?
                CustomData.ProjectCache[projectSupport.ProjectId] :
                CustomData.ProjectCache[projectSupport.ProjectId] = await ServiceProvider.GetRequiredService<IProjectRepository>().RequireProject(projectSupport.ProjectId);

            foreach(var supportProvidedId in projectSupport.SupportProvidedIds)
            {
                var rawDataLine = new SupportHoursSpentReportData.RawDataLine
                {
                    DateOfSupport = projectSupport.Date,
                    ProjectCode = project.PrefixedNumber,
                    SupportProvidedId = supportProvidedId,
                    TimeInHours = timeInHours
                };
                CustomData.RawDataLines.Add(rawDataLine);
            }
        }

        public override async Task RenderStep()
        {
            if (Workbook == null)
            {
                Workbook = new ExcelWorkbook();
                var _worksheet = Workbook.GetOrCreateWorksheet();
                
                _worksheet.Cell(1, 01).SetValue("Date of Support").SetBold(true);
                _worksheet.Cell(1, 02).SetValue("Time in Hours").SetBold(true);
                _worksheet.Cell(1, 03).SetValue("Project").SetBold(true);
                _worksheet.Cell(1, 04).SetValue("Support Provided").SetBold(true);

                RowIndex = 2;
            }

            var worksheet = Workbook.GetWorksheet();

            var stepTimestamp = MicrosecondTimer.Timestamp;
            var stepItems = 0;
            do
            {
                var staticData = await ServiceProvider.GetRequiredService<IStaticDataService>().GetStaticData();

                if (StageCount >= CustomData.RawDataLines.Count)
                {
                    GenerateResult();
                    Stage = ReportStage.Complete;
                    Complete = true;
                    break;
                }

                foreach (var rawDataLine in CustomData.RawDataLines)
                {
                    var supportProvided = staticData.SupportProvided.FirstOrDefault(p => p.Id == rawDataLine.SupportProvidedId);

                    worksheet.Cell(RowIndex, 01).SetValue(rawDataLine.DateOfSupport);
                    worksheet.Cell(RowIndex, 02).SetValue(rawDataLine.TimeInHours);
                    worksheet.Cell(RowIndex, 03).SetValue(rawDataLine.ProjectCode);
                    worksheet.Cell(RowIndex, 04).SetValue(supportProvided?.Name ?? "Invalid Support Provided Type");

                    RowIndex++;
                }

                StageCount++;
                stepItems++;
            }
            while (MicrosecondTimer.Timestamp - stepTimestamp < MICROSECONDS_PER_STEP);

            Metrics.AddRender(MicrosecondTimer.Timestamp - stepTimestamp, stepItems);
        }

        public override void FormatWorksheet(ExcelWorksheet worksheet)
        {
            worksheet.Column(01).AdjustToContents();
            worksheet.Column(02).AdjustToContents();
            worksheet.Column(03).AdjustToContents();
            worksheet.Column(04).AdjustToContents();
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

        public override object ExtractReportData()
        {
            return CustomData;
        }
    }
}
