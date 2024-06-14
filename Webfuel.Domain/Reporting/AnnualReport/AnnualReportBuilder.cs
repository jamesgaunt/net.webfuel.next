using Azure.Core;
using DocumentFormat.OpenXml.Office2010.Drawing;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using Webfuel.Domain.StaticData;
using Webfuel.Excel;
using Webfuel.Reporting;

namespace Webfuel.Domain
{
    public static class AnnualReportStage
    {
        public const string Initialising = "Initialising";

        public const string ApplicationSupport = "Application Support";

        public const string Workforce = "Workforce";

        public const string Validating = "Validating";

        public const string Complete = "Complete";
    }

    public partial class AnnualReportBuilder : ReportBuilderBase
    {
        const long MICROSECONDS_PER_LOAD = 1000 * 25;

        const long MICROSECONDS_PER_STEP = 1000 * 100;

        protected int ItemsPerLoad { get; set; } = 10; // Initial value, will auto-tune

        public AnnualReportBuilder(IServiceProvider serviceProvider, AnnualReportSettings settings)
        {
            ServiceProvider = serviceProvider;
            Stage = AnnualReportStage.Initialising;
            Settings = settings;
            Query = new Query();

            // If we have a FromDate exclude projects that were closed before this date
            if (settings.FromDate.HasValue) {
                Query.Any(a =>
                {
                    a.Equal(nameof(Project.ClosureDate), null, true);
                    a.GreaterThanOrEqual(nameof(Project.ClosureDate), settings.FromDate.Value, true);
                });
            }

            // If we have a ToDate exclude projects that were opened after this date
            if (settings.ToDate.HasValue) {
                Query.LessThanOrEqual(nameof(Project.DateOfRequest), settings.ToDate.Value, true);
            }
        }

        public Query Query { get; set; }

        internal AnnualReportWorkbook? Workbook { get; set; }

        internal AnnualReportContext? Context { get; set; }

        internal AnnualReportSettings Settings { get; set; }

        // Generation

        public override async Task GenerateReport()
        {
            if (Stage == AnnualReportStage.Initialising)
                await Stage_Initialising();
            else if (Stage == AnnualReportStage.ApplicationSupport)
                await Stage_ApplicationSupport();
            else if (Stage == AnnualReportStage.Workforce)
                await Stage_Workforce();
            else if (Stage == AnnualReportStage.Validating)
                await Stage_Validating();
            else
                throw new InvalidOperationException($"Invalid Stage: {Stage}");
        }

        // Stages

        async Task Stage_Initialising()
        {
            Workbook = new AnnualReportWorkbook();
            Context = await AnnualReportContext.Create(ServiceProvider, Settings);

            await Stage_ApplicationSupport();
        }

        async Task Stage_ApplicationSupport()
        {
            if(Workbook == null || Context == null)
                throw new InvalidOperationException("Report is not initialised");

            if (Stage != AnnualReportStage.ApplicationSupport)
            {
                Stage = AnnualReportStage.ApplicationSupport;
                StageTotal = await GetTotalProjectCount();
                StageCount = 0;
                return;
            }

            var stepTimestamp = MicrosecondTimer.Timestamp;
            var stepItems = 0;
            do
            {
                var loadTimestamp = MicrosecondTimer.Timestamp;
                var projects = await QueryProjects(StageCount, ItemsPerLoad);
                var loadMicroseconds = MicrosecondTimer.Timestamp - loadTimestamp;

                var none = true;
                var loadItems = 0;
                foreach (var project in projects)
                {
                    stepItems++;
                    loadItems++;
                    none = false;

                    // Should we include this project in the report?
                    if (Context.Settings.FromDate.HasValue && project.ClosureDate < Context.Settings.FromDate.Value)
                        continue;
                    if(Context.Settings.ToDate.HasValue && project.DateOfRequest > Context.Settings.ToDate.Value)
                        continue;
                    if (project.StatusId == ProjectStatusEnum.Discarded)
                        continue;

                    await Workbook.ApplicationSupport.RenderProject(ServiceProvider, Context, project);
                }

                if (none)
                {
                    await Stage_Workforce();
                    break;
                }

                StageCount += loadItems;
                Metrics.AddLoad(loadMicroseconds, loadItems);
                TuneItemsPerLoad(loadMicroseconds);
            }
            while (MicrosecondTimer.Timestamp - stepTimestamp < MICROSECONDS_PER_STEP);

            Metrics.AddGeneration(MicrosecondTimer.Timestamp - stepTimestamp, stepItems);
        }

        async Task Stage_Workforce()
        {
            if (Workbook == null || Context == null)
                throw new InvalidOperationException("Report is not initialised");

            if (Stage != AnnualReportStage.Workforce)
            {
                Stage = AnnualReportStage.Workforce;
                StageTotal = Context.Users.Count;
                StageCount = 0;
                return;
            }

            var stepTimestamp = MicrosecondTimer.Timestamp;
            var stepItems = 0;
            do
            {
                var loadTimestamp = MicrosecondTimer.Timestamp;
                var users = Context.Users.Skip(StageCount).Take(10);
                var loadMicroseconds = MicrosecondTimer.Timestamp - loadTimestamp;

                var none = true;
                var loadItems = 0;
                foreach (var user in users)
                {
                    none = false;
                    stepItems++;
                    loadItems++;

                    // Should we include this user in the report?
                    //if (!user.IncludeInWorkforceReport)
                    //    continue;

                    await Workbook.Workforce.RenderUser(ServiceProvider, Context, user);
                }

                if (none)
                {
                    await Stage_Validating();
                    break;
                }

                StageCount += loadItems;
            }
            while (MicrosecondTimer.Timestamp - stepTimestamp < MICROSECONDS_PER_STEP);

            Metrics.AddGeneration(MicrosecondTimer.Timestamp - stepTimestamp, stepItems);
        }

        async Task Stage_Validating()
        {
            if(Stage != AnnualReportStage.Validating)
            {
                StageTotal = 0;
                StageCount = 0;
                Stage = AnnualReportStage.Validating;
                return;
            }

            if (Context?.Settings.HighlightInvalidCells == true)
            {
                Workbook?.ApplicationSupport.Validate();
                Workbook?.Workforce.Validate();
            }

            Stage = AnnualReportStage.Complete;
            Complete = true;

            await Task.Delay(1000);
        }
         
        public override ReportResult RenderReport()
        {
            if (Workbook == null)
                throw new InvalidOperationException("Workbook is null");

            return new ReportResult
            {
                MemoryStream = Workbook.Workbook.ToMemoryStream(),
                Filename = "report.xlsx",
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            };
        }

        // Data Querying

        async Task<IEnumerable<Project>> QueryProjects(int skip, int take)
        {
            Query.Skip = StageCount;
            Query.Take = ItemsPerLoad;

            return (await ServiceProvider.GetRequiredService<IProjectRepository>().QueryProject(Query, selectItems: true, countTotal: false)).Items;
        }

        async Task<int> GetTotalProjectCount()
        {
            return (await ServiceProvider.GetRequiredService<IProjectRepository>().QueryProject(Query, selectItems: false, countTotal: true)).TotalCount;
        }

        void TuneItemsPerLoad(long microseconds)
        {
            long microsecondsPerItem = microseconds / ItemsPerLoad;

            ItemsPerLoad = (int)(MICROSECONDS_PER_LOAD / microsecondsPerItem);

            if (ItemsPerLoad < 1)
                ItemsPerLoad = 1;
            if (ItemsPerLoad > 100)
                ItemsPerLoad = 100;
        }
    }      
}
