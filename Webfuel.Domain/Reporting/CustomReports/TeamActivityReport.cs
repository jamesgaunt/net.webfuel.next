using Azure.Core;
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
    internal class TeamActivityReportArguments
    {
        public Guid? SupportTeamId { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public bool DoNotRender { get; set; }
    }

    internal class TeamActivityReportData
    {
        public List<UserData> Users { get; set; } = new List<UserData>();

        // Structure

        public class UserData
        {
            public required User User { get; init; }

            public decimal ProjectSupportHours { get; set; }

            public decimal UserActivityHours { get; set; }
        }
    }

    internal class TeamActivityReportBuilder : ReportBuilder
    {
        public TeamActivityReportBuilder(ReportRequest request) : base(request)
        {
        }

        TeamActivityReportArguments Arguments = new TeamActivityReportArguments();

        TeamActivityReportData CustomData = new TeamActivityReportData();

        string GenerationSubStage = "ProjectSupport"; // ProjectSupport || UserActivity || Complete

        async Task ExtractArguments()
        {
            if (Request.TypedArguments is TeamActivityReportArguments typedArguments)
            {
                Arguments = typedArguments;
            }
            else
            {
                Arguments.StartDate = Request.Arguments.Single(p => p.Name == "Start Date").DateValue;
                Arguments.EndDate = Request.Arguments.Single(p => p.Name == "End Date").DateValue;

                var supportTeamArgument = Request.Arguments.Single(p => p.Name == "Support Team");
                var supportTeamCondition = supportTeamArgument.Conditions.Single(p => p.Value == supportTeamArgument.Condition);

                var staticData = await ServiceProvider.GetRequiredService<IStaticDataService>().GetStaticData();
                var supportTeam = staticData.SupportTeam.Single(p => p.Name == supportTeamCondition.Description);

                Arguments.SupportTeamId = supportTeam.Id;
            }
        }

        async Task ExtractUsers()
        {
            // Extract the population of users we are looking at here, into the Users list in BuilderData

            var users = await ServiceProvider.GetRequiredService<IUserRepository>().SelectUser();

            if (Arguments.SupportTeamId == null)
            {
                CustomData.Users.AddRange(users
                    .Select(p => new TeamActivityReportData.UserData { User = p })
                );
            }
            else
            {
                var supportTeamUsers = await ServiceProvider.GetRequiredService<ISupportTeamUserRepository>().SelectSupportTeamUser();

                CustomData.Users.AddRange(users
                    .Where(u => supportTeamUsers.Any(s => s.UserId == u.Id && s.SupportTeamId == Arguments.SupportTeamId))
                    .Select(u => new TeamActivityReportData.UserData { User = u })
                );
            }

            CustomData.Users = CustomData.Users.OrderBy(p => p.User.LastName).ToList();
        }

        public override async Task InitialisationStep()
        {
            await base.InitialisationStep();
            await ExtractArguments();
            await ExtractUsers();
        }

        int SkipOffset = 0;

        protected override async Task<IEnumerable<object>> QueryItems(int skip, int take)
        {
            if (GenerationSubStage == "ProjectSupport")
            {
                var projectSupportQuery = new Query();
                projectSupportQuery.GreaterThanOrEqual(nameof(ProjectSupport.Date), Arguments.StartDate, Arguments.StartDate != null);
                projectSupportQuery.LessThanOrEqual(nameof(ProjectSupport.Date), Arguments.EndDate, Arguments.EndDate != null);
                projectSupportQuery.Skip = skip - SkipOffset;
                projectSupportQuery.Take = take;

                var projectSupport = await ServiceProvider.GetRequiredService<IProjectSupportRepository>().QueryProjectSupport(projectSupportQuery, selectItems: true, countTotal: false);
                if (projectSupport.Items.Count > 0)
                    return projectSupport.Items;

                SkipOffset = skip;
                GenerationSubStage = "UserActivity";
            }

            if (GenerationSubStage == "UserActivity")
            {
                var userActivityQuery = new Query();
                userActivityQuery.GreaterThanOrEqual(nameof(UserActivity.Date), Arguments.StartDate, Arguments.StartDate != null);
                userActivityQuery.LessThanOrEqual(nameof(UserActivity.Date), Arguments.EndDate, Arguments.EndDate != null);
                userActivityQuery.Skip = skip - SkipOffset;
                userActivityQuery.Take = take;

                var userActivity = await ServiceProvider.GetRequiredService<IUserActivityRepository>().QueryUserActivity(userActivityQuery, selectItems: true, countTotal: false);
                if (userActivity.Items.Count > 0)
                    return userActivity.Items;

                SkipOffset = skip;
                GenerationSubStage = "Complete";
            }

            return new List<object>();
        }

        protected override async Task<int> GetTotalCount()
        {
            var projectSupportQuery = new Query();
            projectSupportQuery.GreaterThanOrEqual(nameof(ProjectSupport.Date), Arguments.StartDate, Arguments.StartDate != null);
            projectSupportQuery.LessThanOrEqual(nameof(ProjectSupport.Date), Arguments.EndDate, Arguments.EndDate != null);

            var userActivityQuery = new Query();
            userActivityQuery.GreaterThanOrEqual(nameof(UserActivity.Date), Arguments.StartDate, Arguments.StartDate != null);
            userActivityQuery.LessThanOrEqual(nameof(UserActivity.Date), Arguments.EndDate, Arguments.EndDate != null);

            var projectSupport = await ServiceProvider.GetRequiredService<IProjectSupportRepository>().QueryProjectSupport(projectSupportQuery, selectItems: false, countTotal: true);
            var userActivity = await ServiceProvider.GetRequiredService<IUserActivityRepository>().QueryUserActivity(userActivityQuery, selectItems: false, countTotal: true);

            return projectSupport.TotalCount + userActivity.TotalCount;
        }

        public override ValueTask ProcessItem(object item)
        {
            var projectSupport = item as ProjectSupport;
            if (projectSupport != null)
            {
                foreach (var userData in CustomData.Users)
                {
                    if (projectSupport.AdviserIds.Contains(userData.User.Id))
                        userData.ProjectSupportHours += projectSupport.WorkTimeInHours;
                }
            }

            var userActivity = item as UserActivity;
            if (userActivity != null)
            {
                foreach (var userData in CustomData.Users)
                {
                    if (userActivity.UserId == userData.User.Id)
                        userData.UserActivityHours += userActivity.WorkTimeInHours;
                }
            }

            return ValueTask.CompletedTask;
        }

        public override Task RenderStep()
        {
            if (Arguments.DoNotRender)
            {
                Stage = ReportStage.Complete;
                Complete = true;
                return Task.CompletedTask; // We only intend to extract the raw data
            }

            if (Workbook == null)
            {
                Workbook = new ExcelWorkbook();
                var _worksheet = Workbook.GetOrCreateWorksheet();

                _worksheet.Cell(1, 01).SetValue("Advisor").SetBold(true);
                _worksheet.Cell(1, 02).SetValue("Project Support Hours").SetBold(true);
                _worksheet.Cell(1, 03).SetValue("User Activity Hours").SetBold(true);
                _worksheet.Cell(1, 04).SetValue("Total Hours").SetBold(true);

                RowIndex = 2;
            }

            var worksheet = Workbook.GetWorksheet();

            var stepTimestamp = MicrosecondTimer.Timestamp;
            var stepItems = 0;
            do
            {
                if (StageCount >= CustomData.Users.Count)
                {
                    GenerateResult();
                    Stage = ReportStage.Complete;
                    Complete = true;
                    break;
                }

                var userData = CustomData.Users[StageCount];

                worksheet.Cell(RowIndex, 01).SetValue(userData.User.FullName);
                worksheet.Cell(RowIndex, 02).SetValue(userData.ProjectSupportHours);
                worksheet.Cell(RowIndex, 03).SetValue(userData.UserActivityHours);
                worksheet.Cell(RowIndex, 04).SetValue(userData.ProjectSupportHours + userData.UserActivityHours);
                RowIndex++;

                StageCount++;
                stepItems++;
            }
            while (MicrosecondTimer.Timestamp - stepTimestamp < MICROSECONDS_PER_STEP);

            Metrics.AddRender(MicrosecondTimer.Timestamp - stepTimestamp, stepItems);
            return Task.CompletedTask;
        }

        public override void FormatWorksheet(ExcelWorksheet worksheet)
        {
            worksheet.Column(01).AdjustToContents();
            worksheet.Column(02).AdjustToContents();
            worksheet.Column(03).AdjustToContents();
            worksheet.Column(04).AdjustToContents();
        }

        public static async Task<List<ReportArgument>> GenerateArguments(ReportDesign design, IServiceProvider serviceProvider)
        {
            var result = new List<ReportArgument>();

            {
                var staticData = await serviceProvider.GetRequiredService<IStaticDataService>().GetStaticData();
                var supportTeams = staticData.SupportTeam;
                var conditions = new List<ReportFilterCondition>();

                foreach (var supportTeam in supportTeams)
                {
                    conditions.Add(new ReportFilterCondition
                    {
                        Value = conditions.Count(),
                        Description = supportTeam.Name,
                        Unary = true
                    });
                }

                result.Add(new ReportArgument
                {
                    Name = "Support Team",
                    FilterId = Guid.NewGuid(),
                    FieldId = Guid.NewGuid(),
                    Conditions = conditions,
                    ReportProviderId = ReportProviderEnum.CustomReport,
                    FieldType = ReportFieldType.String,
                    FilterType = ReportFilterType.String
                });
            }

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

            return result;
        }

        public override object ExtractReportData()
        {
            return CustomData;
        }
    }
}
