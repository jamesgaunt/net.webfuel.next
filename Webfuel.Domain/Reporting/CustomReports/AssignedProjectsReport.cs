using Microsoft.Extensions.DependencyInjection;
using Webfuel.Domain.StaticData;
using Webfuel.Excel;
using Webfuel.Reporting;

namespace Webfuel.Domain
{
    internal class AssignedProjectsReportArguments
    {
        public Guid? SupportTeamId { get; set; }
    }

    internal class AssignedProjectsReportData
    {
        public List<UserData> Users { get; set; } = new List<UserData>();

        public class UserData
        {
            public required User User { get; init; }

            public StatusData ActiveStatusData { get; } = new StatusData();

            public StatusData OnHoldStatusData { get; } = new StatusData();
        }

        public class StatusData
        {
            public int LeadAdvisorCount { get; set; }

            public int SupportAdvisorCount { get; set; }

            public List<string> LeadAdvisorClients { get; } = new List<string>();

            public List<string> SupportAdvisorClients { get; } = new List<string>();
        }
    }

    internal class AssignedProjectsReportBuilder : ReportBuilder
    {
        public AssignedProjectsReportBuilder(ReportRequest request) : base(request)
        {
            Query = new Query();
            Query.Any(a =>
            {
                a.Equal(nameof(Project.StatusId), ProjectStatusEnum.Active);
                a.Equal(nameof(Project.StatusId), ProjectStatusEnum.OnHold);
                a.Equal(nameof(Project.StatusId), ProjectStatusEnum.SubmittedOnHold);
            });
        }

        AssignedProjectsReportArguments Arguments = new AssignedProjectsReportArguments();

        AssignedProjectsReportData CustomData = new AssignedProjectsReportData();

        async Task ExtractArguments()
        {
            if (Request.TypedArguments is AssignedProjectsReportArguments typedArguments)
            {
                Arguments = typedArguments;
            }
            else
            {
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
                    .Select(p => new AssignedProjectsReportData.UserData { User = p })
                );
            }
            else
            {
                var supportTeamUsers = await ServiceProvider.GetRequiredService<ISupportTeamUserRepository>().SelectSupportTeamUser();

                CustomData.Users.AddRange(users
                    .Where(u => supportTeamUsers.Any(s => s.UserId == u.Id && s.SupportTeamId == Arguments.SupportTeamId))
                    .Select(u => new AssignedProjectsReportData.UserData { User = u })
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

        protected override async Task<IEnumerable<object>> QueryItems(int skip, int take)
        {
            Query.Skip = skip;
            Query.Take = take;
            var result = await ServiceProvider.GetRequiredService<IProjectRepository>().QueryProject(Query, selectItems: true, countTotal: false);
            return result.Items;
        }

        protected override async Task<int> GetTotalCount()
        {
            var result = await ServiceProvider.GetRequiredService<IProjectRepository>().QueryProject(Query, selectItems: false, countTotal: true);
            return result.TotalCount;
        }

        public override async ValueTask ProcessItem(object item)
        {
            var project = item as Project;
            if (project == null)
                return;

            var projectAdvisers = await ServiceProvider.GetRequiredService<IProjectAdviserRepository>().SelectProjectAdviserByProjectId(project.Id);

            foreach (var user in CustomData.Users)
            {
                if (project.LeadAdviserUserId == user.User.Id)
                {
                    if(project.StatusId == ProjectStatusEnum.Active)
                    {
                        user.ActiveStatusData.LeadAdvisorCount++;
                        user.ActiveStatusData.LeadAdvisorClients.Add(project.TeamContactFullName);
                    }
                    else if(project.StatusId == ProjectStatusEnum.OnHold || project.StatusId == ProjectStatusEnum.SubmittedOnHold)
                    {
                        user.OnHoldStatusData.LeadAdvisorCount++;
                        user.OnHoldStatusData.LeadAdvisorClients.Add(project.TeamContactFullName);
                    }
                }
                else if (projectAdvisers.Any(p => p.UserId == user.User.Id))
                {
                    if (project.StatusId == ProjectStatusEnum.Active)
                    {
                        user.ActiveStatusData.SupportAdvisorCount++;
                        user.ActiveStatusData.SupportAdvisorClients.Add(project.TeamContactFullName);
                    }
                    else if (project.StatusId == ProjectStatusEnum.OnHold || project.StatusId == ProjectStatusEnum.SubmittedOnHold)
                    {
                        user.OnHoldStatusData.SupportAdvisorCount++;
                        user.OnHoldStatusData.SupportAdvisorClients.Add(project.TeamContactFullName);
                    }

                }
            }
        }

        public int ActiveRowIndex = 2;
        public int OnHoldRowIndex = 2;

        public override async Task RenderStep()
        {
            if (Workbook == null)
            {
                Workbook = new ExcelWorkbook();

                var _worksheet = Workbook.GetOrCreateWorksheet("Active Projects");
                
                _worksheet.Cell(1, 01).SetValue("Advisor").SetBold(true);
                _worksheet.Cell(1, 02).SetValue("Lead Adviser").SetBold(true);
                _worksheet.Cell(1, 03).SetValue("Lead Adviser Client Name(s)").SetBold(true);
                _worksheet.Cell(1, 04).SetValue("Support Adviser").SetBold(true);
                _worksheet.Cell(1, 05).SetValue("Support Adviser Client Name(s)").SetBold(true);

                _worksheet = Workbook.GetOrCreateWorksheet("On Hold Projects");
                _worksheet.Cell(1, 01).SetValue("Advisor").SetBold(true);
                _worksheet.Cell(1, 02).SetValue("Lead Adviser").SetBold(true);
                _worksheet.Cell(1, 03).SetValue("Lead Adviser Client Name(s)").SetBold(true);
                _worksheet.Cell(1, 04).SetValue("Support Adviser").SetBold(true);
                _worksheet.Cell(1, 05).SetValue("Support Adviser Client Name(s)").SetBold(true);
            }

            var activeWorksheet = Workbook.GetWorksheet("Active Projects");
            var onHoldWorksheet = Workbook.GetWorksheet("On Hold Projects");

            var stepTimestamp = MicrosecondTimer.Timestamp;
            var stepItems = 0;
            do
            {
                if (StageCount >= CustomData.Users.Count)
                {
                    await FormatWorkbook();

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

                var userData = CustomData.Users[StageCount];

                if(userData.ActiveStatusData.LeadAdvisorCount > 0 || userData.ActiveStatusData.SupportAdvisorCount > 0)
                {
                    activeWorksheet.Cell(ActiveRowIndex, 01).SetValue(userData.User.FullName).VerticalTopAlign();
                    activeWorksheet.Cell(ActiveRowIndex, 02).SetValue(userData.ActiveStatusData.LeadAdvisorCount).VerticalTopAlign();
                    activeWorksheet.Cell(ActiveRowIndex, 03).SetValue(String.Join(", ", userData.ActiveStatusData.LeadAdvisorClients.Distinct())).SetTextWrap(true).VerticalTopAlign();
                    activeWorksheet.Cell(ActiveRowIndex, 04).SetValue(userData.ActiveStatusData.SupportAdvisorCount).VerticalTopAlign();
                    activeWorksheet.Cell(ActiveRowIndex, 05).SetValue(String.Join(", ", userData.ActiveStatusData.SupportAdvisorClients.Distinct())).SetTextWrap(true).VerticalTopAlign();
                    ActiveRowIndex++;
                }

                if (userData.OnHoldStatusData.LeadAdvisorCount > 0 || userData.OnHoldStatusData.SupportAdvisorCount > 0)
                {
                    onHoldWorksheet.Cell(OnHoldRowIndex, 01).SetValue(userData.User.FullName).VerticalTopAlign();
                    onHoldWorksheet.Cell(OnHoldRowIndex, 02).SetValue(userData.OnHoldStatusData.LeadAdvisorCount).VerticalTopAlign();
                    onHoldWorksheet.Cell(OnHoldRowIndex, 03).SetValue(String.Join(", ", userData.OnHoldStatusData.LeadAdvisorClients.Distinct())).SetTextWrap(true).VerticalTopAlign();
                    onHoldWorksheet.Cell(OnHoldRowIndex, 04).SetValue(userData.OnHoldStatusData.SupportAdvisorCount).VerticalTopAlign();
                    onHoldWorksheet.Cell(OnHoldRowIndex, 05).SetValue(String.Join(", ", userData.OnHoldStatusData.SupportAdvisorClients.Distinct())).SetTextWrap(true).VerticalTopAlign();
                    OnHoldRowIndex++;
                }

                StageCount++;
                stepItems++;
            }
            while (MicrosecondTimer.Timestamp - stepTimestamp < MICROSECONDS_PER_STEP);

            Metrics.AddRender(MicrosecondTimer.Timestamp - stepTimestamp, stepItems);
        }

        async Task FormatWorkbook()
        {
            var activeWorksheet = Workbook!.GetWorksheet("Active Projects");
            var onHoldWorksheet = Workbook!.GetWorksheet("On Hold Projects");

            activeWorksheet.Column(01).AdjustToContents();
            activeWorksheet.Column(02).AdjustToContents();
            activeWorksheet.Column(03).SetWidth(40);
            activeWorksheet.Column(04).AdjustToContents();
            activeWorksheet.Column(05).SetWidth(40);

            onHoldWorksheet.Column(01).AdjustToContents();
            onHoldWorksheet.Column(02).AdjustToContents();
            onHoldWorksheet.Column(03).SetWidth(40);
            onHoldWorksheet.Column(04).AdjustToContents();
            onHoldWorksheet.Column(05).SetWidth(40);

            activeWorksheet.Cell(ActiveRowIndex, 01).SetValue("Total Active Projects").SetBold(true);
            activeWorksheet.Cell(ActiveRowIndex, 02).SetValue(CustomData.Users.Sum(p => p.ActiveStatusData.LeadAdvisorCount)).SetBold(true);
            activeWorksheet.Cell(ActiveRowIndex, 04).SetValue(CustomData.Users.Sum(p => p.ActiveStatusData.SupportAdvisorCount)).SetBold(true);

            onHoldWorksheet.Cell(ActiveRowIndex, 01).SetValue("Total OnHold Projects").SetBold(true);
            onHoldWorksheet.Cell(ActiveRowIndex, 02).SetValue(CustomData.Users.Sum(p => p.OnHoldStatusData.LeadAdvisorCount)).SetBold(true);
            onHoldWorksheet.Cell(ActiveRowIndex, 04).SetValue(CustomData.Users.Sum(p => p.OnHoldStatusData.SupportAdvisorCount)).SetBold(true);

            var headlines = Workbook!.GetOrCreateWorksheet("Headlines");
            headlines.Position = 1;

            var supportTeam = await ServiceProvider.GetRequiredService<IStaticDataService>().GetSupportTeam(Arguments.SupportTeamId ?? Guid.Empty);

            headlines.Column(1).SetWidth(20);
            headlines.Column(2).SetWidth(20);
            headlines.Column(3).SetWidth(20);

            headlines.Cell("A1").SetValue($"{supportTeam?.Name ?? "All Advisers"} Workload {DateTime.Today.ToString("dd MMM yyyy")}").SetBold(true);

            headlines.Cell("A3").SetValue("Role").SetBold(true);
            headlines.Cell("B3").SetValue("Status").SetBold(true);
            headlines.Cell("C3").SetValue("Number of Projects").SetBold(true);

            headlines.Cell("A4").SetValue("Lead Advisor");
            headlines.Cell("B4").SetValue("Active");
            headlines.Cell("C4").SetValue(CustomData.Users.Sum(p => p.ActiveStatusData.LeadAdvisorCount));

            headlines.Cell("A5").SetValue("Support Advisor");
            headlines.Cell("B5").SetValue("Active");
            headlines.Cell("C5").SetValue(CustomData.Users.Sum(p => p.ActiveStatusData.SupportAdvisorCount));

            headlines.Cell("A6").SetValue("Lead Advisor");
            headlines.Cell("B6").SetValue("On Hold");
            headlines.Cell("C6").SetValue(CustomData.Users.Sum(p => p.OnHoldStatusData.LeadAdvisorCount));

            headlines.Cell("A7").SetValue("Support Advisor");
            headlines.Cell("B7").SetValue("On Hold");
            headlines.Cell("C7").SetValue(CustomData.Users.Sum(p => p.OnHoldStatusData.SupportAdvisorCount));
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

            return result;
        }

        public override object ExtractReportData()
        {
            return CustomData;
        }
    }
}

