using Azure.Core;
using DocumentFormat.OpenXml.Office2010.Drawing;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Extensions.DependencyInjection;
using Webfuel.Domain.StaticData;
using Webfuel.Excel;
using Webfuel.Reporting;

namespace Webfuel.Domain
{
    static class AnnualReportBuilderExtensions
    {
        public static ExcelCell SetBoolean(this ExcelCell cell, bool value)
        {
            cell.SetValue(value ? "Y" : "N");
            cell.CentreAlign();
            if (value)
                cell.SetBackgroundColor(System.Drawing.Color.LightGreen);
            return cell;
        }
    }

    public class AnnualReportBuilder : ReportBuilderBase
    {
        const double STANDARD_WIDTH = 14D;
        const double BOOLEAN_WIDTH = 3D;

        readonly System.Drawing.Color BlueBG = System.Drawing.Color.FromArgb(184, 204, 228);

        const long MICROSECONDS_PER_LOAD = 1000 * 25;

        const long MICROSECONDS_PER_STEP = 1000 * 100;

        protected int ItemsPerLoad { get; set; } = 10; // Initial value, will auto-tune

        private readonly IStaticDataModel StaticData;

        private readonly IReadOnlyList<User> Users;

        public AnnualReportBuilder(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            Stage = ReportStage.Initialisation;

            StaticData = serviceProvider.GetRequiredService<IStaticDataService>().GetStaticData().Result;
            Users = serviceProvider.GetRequiredService<IUserRepository>().SelectUser().Result;

            Query = new Query();
        }

        public DateOnly? FromDate { get; set; }

        public DateOnly? ToDate { get; set; }

        public Query Query { get; set; }

        public List<AnnualReportData> Data { get; } = new List<AnnualReportData>();

        public ExcelWorkbook? Workbook { get; set; }

        public ReportResult? Result { get; set; }

        // Query

        async Task<IEnumerable<Project>> QueryItems(int skip, int take)
        {
            Query.Skip = StageCount;
            Query.Take = ItemsPerLoad;

            return (await ServiceProvider.GetRequiredService<IProjectRepository>().QueryProject(Query, selectItems: true, countTotal: false)).Items;
        }

        async Task<int> GetTotalCount()
        {
            return (await ServiceProvider.GetRequiredService<IProjectRepository>().QueryProject(Query, selectItems: false, countTotal: true)).TotalCount;
        }

        // Generation

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
                    var data = await GenerateData(item);
                    Data.Add(data);

                    none = false;
                    stepItems++;
                    loadItems++;
                }

                if (none)
                {
                    StageTotal = Data.Count;
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

        public Task RenderStep()
        {
            if (Workbook == null)
            {
                Workbook = new ExcelWorkbook();
                var _worksheet = Workbook.GetOrCreateWorksheet();
                GenerateHeaders(_worksheet);
            }

            var worksheet = Workbook.GetOrCreateWorksheet();

            var stepTimestamp = MicrosecondTimer.Timestamp;
            var stepItems = 0;
            do
            {
                if (StageCount >= Data.Count)
                {
                    GenerateResult();
                    Stage = ReportStage.Complete;
                    Complete = true;
                    break;
                }

                GenerateRow(worksheet, StageCount);

                StageCount++;
                stepItems++;
            }
            while (MicrosecondTimer.Timestamp - stepTimestamp < MICROSECONDS_PER_STEP);

            Metrics.AddRender(MicrosecondTimer.Timestamp - stepTimestamp, stepItems);
            return Task.CompletedTask;
        }

        void GenerateHeaders(ExcelWorksheet worksheet)
        {
            worksheet.Row(1).SetTextRotation(90).CentreAlign();

            // Section A
            worksheet.Cell(1, AXXX).SetValue("SECTION A").SetWidth(BOOLEAN_WIDTH).SetBackgroundColor(BlueBG);
            worksheet.Cell(1, A001).SetValue("Project ID").SetWidth(STANDARD_WIDTH);
            worksheet.Cell(1, A002).SetValue("Date of first contact").SetWidth(STANDARD_WIDTH);
            worksheet.Cell(1, A003).SetValue("Closure date (full-stage outcome known or otherwise closed)").SetWidth(STANDARD_WIDTH);
            worksheet.Cell(1, A004).SetValue("NIHR application ID (if known)").SetWidth(STANDARD_WIDTH);
            worksheet.Cell(1, A005).SetValue("Application destination (NIHR programme or other funder name)").SetWidth(STANDARD_WIDTH);
            worksheet.Cell(1, A006).SetValue("Submitted (Y/N/Don't Know/Not applicable)").SetWidth(STANDARD_WIDTH);
            worksheet.Cell(1, A007).SetValue("Comment on application destination").SetWidth(STANDARD_WIDTH);
            worksheet.Cell(1, A008).SetValue("Submission stage (outline/full/one-stage/not applicable/unknown)").SetWidth(STANDARD_WIDTH);
            worksheet.Cell(1, A009).SetValue("Outcome (shortlisted/rejected/not submitted/not applicable)").SetWidth(STANDARD_WIDTH);
            worksheet.Cell(1, A010).SetValue("Is a paid RSS staff member, who provided advice, the lead/co-lead for this application?").SetWidth(BOOLEAN_WIDTH);
            worksheet.Cell(1, A011).SetValue("Is a paid RSS staff member, who provided advice, a co-applicant for this application?").SetWidth(BOOLEAN_WIDTH);
            worksheet.Cell(1, A012).SetValue("Was advice provided (a) pre-award, (b) pre- and post-award or (c) post-award without pre-award support?").SetWidth(STANDARD_WIDTH);

            // Section B
            worksheet.Cell(1, BXXX).SetValue("SECTION B").SetWidth(BOOLEAN_WIDTH).SetBackgroundColor(BlueBG);
            worksheet.Cell(1, B001).SetValue("Total amount of support provided since first contact (low < 10;  medium = 10 < 30, high = 30 hours)?").SetWidth(STANDARD_WIDTH);
            worksheet.Cell(1, B002).SetValue("Categories of support provided (more than one can be selected)").SetWidth(STANDARD_WIDTH);
            worksheet.Cell(1, B003).SetValue("Specific type or discipline of advisor(s) providing advice (more than one can be selected)").SetWidth(STANDARD_WIDTH);
            worksheet.Cell(1, B004).SetValue("Will this study use a CTU?").SetWidth(BOOLEAN_WIDTH);
            worksheet.Cell(1, B005).SetValue("Is this CTU internal or external to the RSS?").SetWidth(STANDARD_WIDTH);
            worksheet.Cell(1, B006).SetValue("Sites involved in providing advice (more than one can be selected)").SetWidth(STANDARD_WIDTH);

            // Section C
            worksheet.Cell(1, CXXX).SetValue("SECTION C").SetWidth(BOOLEAN_WIDTH).SetBackgroundColor(BlueBG);
            worksheet.Cell(1, C001).SetValue("Monetary value of funding application").SetWidth(STANDARD_WIDTH);
            worksheet.Cell(1, C002).SetValue("Project team lead/lead applicant's career stage").SetWidth(STANDARD_WIDTH);
            worksheet.Cell(1, C003).SetValue("Project team lead/lead applicant’s employing organisation (if known)").SetWidth(STANDARD_WIDTH);
            worksheet.Cell(1, C004).SetValue("Project team lead/lead applicant's employing organisation type").SetWidth(STANDARD_WIDTH);
            worksheet.Cell(1, C005).SetValue("Where is the applicant being supported based?").SetWidth(STANDARD_WIDTH);
            worksheet.Cell(1, C006).SetValue("Other - comment").SetWidth(STANDARD_WIDTH);
            worksheet.Cell(1, C007).SetValue("Project team lead/lead applicant’s ORCID (if known)").SetWidth(STANDARD_WIDTH);
            worksheet.Cell(1, C008).SetValue("Professional background of all team/applicants (where known - more than one can be selected)").SetWidth(STANDARD_WIDTH);
            worksheet.Cell(1, C009).SetValue("Has the RSS provided a small PPI grant to support this application?").SetWidth(BOOLEAN_WIDTH);
        }

        void GenerateRow(ExcelWorksheet worksheet, int index)
        {
            var row = index + 2;
            var data = Data[index];

            worksheet.Cell(row, A001).SetValue(data.ProjectID);
            worksheet.Cell(row, A002).SetValue(data.DateOfFirstContact);
            worksheet.Cell(row, A003).SetValue(data.ClosureDate);
            worksheet.Cell(row, A004).SetValue(data.NIHRApplicationID);
            worksheet.Cell(row, A005).SetValue(data.ApplicationDestination);
            worksheet.Cell(row, A006).SetValue(data.Submitted);
            worksheet.Cell(row, A007).SetValue(data.CommentOnApplicationDestination);
            worksheet.Cell(row, A008).SetValue(data.SubmissionStage);
            worksheet.Cell(row, A009).SetValue(data.Outcome);
            worksheet.Cell(row, A010).SetBoolean(data.IsAPaidRSSStaffMemberLead);
            worksheet.Cell(row, A011).SetBoolean(data.IsAPaidRSSStaffMemberCoapplicant);
            worksheet.Cell(row, A012).SetValue(data.WasAdvicePrePostAward);

            worksheet.Cell(row, B001).SetValue(data.TotalAmountOfSupportProvided);
            worksheet.Cell(row, B002).SetValue(data.CategoriesOfSupportProvided);
            worksheet.Cell(row, B003).SetValue(data.AdviserDisciplines);
            worksheet.Cell(row, B004).SetBoolean(data.WillThisStudyUseACTU);
            worksheet.Cell(row, B005).SetValue(data.IsThisCTUInternalOrExternalToTheRSS);
            worksheet.Cell(row, B006).SetValue(data.SitesInvolvedInProvidingAdvice);

            worksheet.Cell(row, C001).SetValue(data.MonetaryValueOfFundingApplication);
            worksheet.Cell(row, C002).SetValue(data.ProjectTeamLeadCareerStage);
            worksheet.Cell(row, C003).SetValue(data.ProjectTeamLeadEmployingOrganisation);
            worksheet.Cell(row, C004).SetValue(data.ProjectTeamLeadEmployingOrganisationType);
            worksheet.Cell(row, C005).SetValue(data.WhereIsSupportBased);
            worksheet.Cell(row, C006).SetValue(data.OtherSupportBased);
            worksheet.Cell(row, C007).SetValue(data.ProjectTeamLeadORCID);
            worksheet.Cell(row, C008).SetValue(data.ProfessionalBackgroundOfApplicants);
            worksheet.Cell(row, C009).SetBoolean(data.HasRSSProvidedPPIGrant);
        }

        public virtual void GenerateResult()
        {
            if (Workbook == null)
                throw new InvalidOperationException("Workbook has not been generated yet");

            Result = new ReportResult
            {
                MemoryStream = Workbook.ToMemoryStream(),
                Filename = "report.xlsx",
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            };
        }

        public override ReportResult RenderReport()
        {
            return Result ?? throw new InvalidOperationException("Report has not been generated yet");
        }

        public async Task<AnnualReportData> GenerateData(Project project)
        {
            var support = await ServiceProvider.GetRequiredService<IProjectSupportRepository>().SelectProjectSupportByProjectId(project.Id);
            var advisers = await ServiceProvider.GetRequiredService<IProjectAdviserRepository>().SelectProjectAdviserByProjectId(project.Id);

            var data = new AnnualReportData
            {
                ProjectID = project.PrefixedNumber,
                DateOfFirstContact = project.DateOfRequest,
                ClosureDate = project.ClosureDate,
                NIHRApplicationID = project.NIHRApplicationId,
                ApplicationDestination = ApplicationDestination(project),
                Submitted = "TODO",
                CommentOnApplicationDestination = CommentOnApplicationDestination(project),
                SubmissionStage = "TODO",
                Outcome = "TODO",
                IsAPaidRSSStaffMemberLead = project.IsPaidRSSAdviserLeadId == IsPaidRSSAdviserLeadEnum.Yes,
                IsAPaidRSSStaffMemberCoapplicant = project.IsPaidRSSAdviserCoapplicantId == IsPaidRSSAdviserCoapplicantEnum.Yes,
                WasAdvicePrePostAward = "TODO",
                TotalAmountOfSupportProvided = TotalAmountOfSupportProvided(project, support),
                CategoriesOfSupportProvided = CategoriesOfSupportProvided(project, support),
                AdviserDisciplines = AdviserDisciplines(project, support, advisers),
                WillThisStudyUseACTU = WillThisStudyUseACTU(project),
                IsThisCTUInternalOrExternalToTheRSS = IsThisCTUInternalOrExternalToTheRSS(project),
                SitesInvolvedInProvidingAdvice = SitesInvolvedInProvidingAdvice(project),
                MonetaryValueOfFundingApplication = project.MonetaryValueOfFundingApplication,
                ProjectTeamLeadCareerStage = project.LeadApplicantCareerStage,
                ProjectTeamLeadEmployingOrganisation = project.LeadApplicantOrganisation,
                ProjectTeamLeadEmployingOrganisationType = ProjectTeamLeadOrganisationType(project),
                WhereIsSupportBased = "Imperial College London and Partners",
                OtherSupportBased = "Not applicable",
                ProjectTeamLeadORCID = project.LeadApplicantORCID,
                ProfessionalBackgroundOfApplicants = ProfessionalBackgroundOfApplicants(project),
                HasRSSProvidedPPIGrant = false
            };

            return data;
        }

        // Helpers

        string ApplicationDestination(Project project)
        {
            if (project.SubmittedFundingStreamId == null)
                return "Unknown";
            var fundingStream = StaticData.FundingStream.Single(fs => fs.Id == project.SubmittedFundingStreamId);
            return fundingStream.Name;
        }

        string CommentOnApplicationDestination(Project project)
        {
            if (project.SubmittedFundingStreamId == null)
                return "Not applicable";

            var fundingStream = StaticData.FundingStream.Single(fs => fs.Id == project.SubmittedFundingStreamId);
            if (!fundingStream.FreeText)
                return "Not applicable";

            if (!String.IsNullOrEmpty(project.SubmittedFundingStreamFreeText))
                return project.SubmittedFundingStreamFreeText;

            return "Not applicable";
        }

        string TotalAmountOfSupportProvided(Project project, List<ProjectSupport> support)
        {
            var total = support.Count == 0 ? 0 : support.Sum(s => s.WorkTimeInHours * s.AdviserIds.Count);
            if (total < 10)
                return "Low";
            if (total < 30)
                return "Medium";
            return "High";
        }

        string CategoriesOfSupportProvided(Project project, List<ProjectSupport> support)
        {
            var categories = support.SelectMany(s => s.SupportProvidedIds).Distinct().Select(id => Format(StaticData.SupportProvided.Single(sc => sc.Id == id)));
            return String.Join(", ", categories.Where(p => p != "IGNORE").Distinct());
        }

        string SitesInvolvedInProvidingAdvice(Project project)
        {
            var hubs = new List<string>();
            hubs.Add("Imperial College London and Partners");
            hubs.AddRange(project.RSSHubProvidingAdviceIds.Select(id => StaticData.RSSHub.Single(h => h.Id == id).Name));
            return String.Join(", ", hubs.Distinct());
        }

        string ProjectTeamLeadOrganisationType(Project project)
        {
            if (project.LeadApplicantOrganisationTypeId == null)
                return "Unknown";

            var organisation = StaticData.ResearcherOrganisationType.Single(o => o.Id == project.LeadApplicantOrganisationTypeId);
            return organisation.Name;
        }

        string ProfessionalBackgroundOfApplicants(Project project)
        {
            var backgrounds = project.ProfessionalBackgroundIds.Distinct().Select(id => StaticData.ProfessionalBackground.Single(pb => pb.Id == id).Name).ToList();
            if (!String.IsNullOrEmpty(project.ProfessionalBackgroundFreeText))
                backgrounds.Add(project.ProfessionalBackgroundFreeText);
            return String.Join(", ", backgrounds);
        }

        bool WillThisStudyUseACTU(Project project)
        {
            if (project.WillStudyUseCTUId == WillStudyUseCTUEnum.YesInternalToThisRSS)
                return true;

            if (project.WillStudyUseCTUId == WillStudyUseCTUEnum.YesExternalToThisRSS)
                return true;

            return false;
        }

        string IsThisCTUInternalOrExternalToTheRSS(Project project)
        {
            if (project.WillStudyUseCTUId == WillStudyUseCTUEnum.YesInternalToThisRSS)
                return "Internal";

            if (project.WillStudyUseCTUId == WillStudyUseCTUEnum.YesExternalToThisRSS)
                return "External";

            return "Not applicable";
        }

        string AdviserDisciplines(Project project, List<ProjectSupport> support, List<ProjectAdviser> advisers)
        {
            var disciplines = new List<string>();

            var userIds = new List<Guid>();
            {
                if (project.LeadAdviserUserId != null)
                    userIds.Add(project.LeadAdviserUserId.Value);
                foreach (var s in support)
                {
                    userIds.AddRange(s.AdviserIds);
                }
                foreach (var a in advisers)
                {
                    userIds.Add(a.UserId);
                }
                userIds = userIds.Distinct().ToList();
            }

            var users = Users.Where(u => userIds.Contains(u.Id)).ToList();

            var disciplineIds = new List<Guid>();
            {
                foreach (var userId in userIds)
                {
                    var user = users.Single(u => u.Id == userId);

                    disciplineIds.AddRange(user.DisciplineIds);
                    if (!String.IsNullOrEmpty(user.DisciplineFreeText))
                        disciplines.Add(user.DisciplineFreeText);
                }

                disciplineIds = disciplineIds.Distinct().ToList();
            }

            disciplines.AddRange(disciplineIds.Select(id => Format(StaticData.UserDiscipline.Single(ud => ud.Id == id))));

            return String.Join(", ", disciplines.Where(p => p != "IGNORE").Distinct());
        }

        string Format(SupportProvided supportProvided)
        {
            if (!String.IsNullOrEmpty(supportProvided.Alias))
                return supportProvided.Alias;
            return supportProvided.Name;
        }

        string Format(UserDiscipline userDiscipline)
        {
            if (!String.IsNullOrEmpty(userDiscipline.Alias))
                return userDiscipline.Alias;
            return userDiscipline.Name;
        }

        // Columns

        // Section A
        const int AXXX = 1;
        const int A001 = 2;
        const int A002 = 3;
        const int A003 = 4;
        const int A004 = 5;
        const int A005 = 6;
        const int A006 = 7;
        const int A007 = 8;
        const int A008 = 9;
        const int A009 = 10;
        const int A010 = 11;
        const int A011 = 12;
        const int A012 = 13;

        // Section B
        const int BXXX = 14;
        const int B001 = 15;
        const int B002 = 16;
        const int B003 = 17;
        const int B004 = 18;
        const int B005 = 19;
        const int B006 = 20;

        // Section C
        const int CXXX = 21;
        const int C001 = 22;
        const int C002 = 23;
        const int C003 = 24;
        const int C004 = 25;
        const int C005 = 26;
        const int C006 = 27;
        const int C007 = 28;
        const int C008 = 29;
        const int C009 = 30;
    }
}
