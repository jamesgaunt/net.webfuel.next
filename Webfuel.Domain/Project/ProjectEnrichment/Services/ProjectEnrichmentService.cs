using Webfuel.Domain.StaticData;
using Webfuel.Jobs;

namespace Webfuel.Domain;

public interface IProjectEnrichmentService
{
    Task CalculateSupportMetricsForProject(Project project);

    Task EnrichProject(Project project);

    Task<BackgroundJobResult> ExecuteHeartbeat();
}

[Service(typeof(IProjectEnrichmentService))]
internal class ProjectEnrichmentService : IProjectEnrichmentService
{
    private readonly IProjectRepository _projectRepository;
    private readonly IProjectSupportRepository _projectSupportRepository;
    private readonly IStaticDataService _staticDataService;

    public ProjectEnrichmentService(
        IProjectRepository projectRepository,
        IProjectSupportRepository projectSupportRepository,
        IStaticDataService staticDataService)
    {
        _projectRepository = projectRepository;
        _projectSupportRepository = projectSupportRepository;
        _staticDataService = staticDataService;
    }

    public async Task CalculateSupportMetricsForProject(Project project)
    {
        project.SupportTotalMinutes = (await _projectSupportRepository.SumMinutesByProjectSupportGroupId(project.ProjectSupportGroupId)) ?? 0;

        var openSupportRequests = await _projectSupportRepository.SelectOpenSupportRequestsByProjectSupportGroupId(project.ProjectSupportGroupId);

        project.OpenSupportRequestTeamIds.Clear();
        project.OverdueSupportRequestTeamIds.Clear();

        var today = DateOnly.FromDateTime(DateTime.Today);

        foreach (var openSupportRequest in openSupportRequests.Where(p => p != null && p.SupportRequestedTeamId != null))
        {
            project.OpenSupportRequestTeamIds.Add(openSupportRequest.SupportRequestedTeamId!.Value);

            if (openSupportRequest.SupportRequestedAt < today.AddDays(-14))
                project.OverdueSupportRequestTeamIds.Add(openSupportRequest.SupportRequestedTeamId!.Value);
        }

        await EnrichProject(project);
    }

    public async Task EnrichProject(Project project)
    {
        var staticData = await _staticDataService.GetStaticData();

        Coerce(project, staticData);
        CalculateEnrichmentDiagnostics(project, staticData);
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Implementation

    void Coerce(Project project, IStaticDataModel staticData)
    {
        var status = staticData.ProjectStatus.First(p => p.Id == project.StatusId);
        project.Locked = status.Locked;
        project.Discarded = status.Discarded;
        project.TeamContactFullName = $"{project.TeamContactTitle} {project.TeamContactFirstName} {project.TeamContactLastName}";
        project.LeadApplicantFullName = $"{project.LeadApplicantTitle} {project.LeadApplicantFirstName} {project.LeadApplicantLastName}";
    }

    void CalculateEnrichmentDiagnostics(Project project, IStaticDataModel staticData)
    {
        var result = new List<ProjectDiagnostic>();

        if (project.Locked)
        {
            project.DiagnosticCount = 0;
            project.DiagnosticList = result;
            return;
        }

        var today = DateOnly.FromDateTime(DateTime.Now);

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Incomplete Fields

        if (String.IsNullOrWhiteSpace(project.Title))
        {
            result.Add(ProjectDiagnostic.EnrichmentWarning("Project title is required"));
        }
        if (project.LeadAdviserUserId == null)
        {
            result.Add(ProjectDiagnostic.EnrichmentWarning("Lead advisor is required"));
        }
        if (project.WillStudyUseCTUId == null)
        {
            result.Add(ProjectDiagnostic.EnrichmentWarning("Will this study use a CTU is required"));
        }
        if (project.MonetaryValueOfFundingApplication == null)
        {
            result.Add(ProjectDiagnostic.EnrichmentWarning("Monetary value of funding application is required"));
        }
        if (project.IsPaidRSSAdviserLeadId == null)
        {
            result.Add(ProjectDiagnostic.EnrichmentWarning("Is paid RSS staff member lead/co-lead is required"));
        }
        if (project.IsPaidRSSAdviserCoapplicantId == null)
        {
            result.Add(ProjectDiagnostic.EnrichmentWarning("Is paid RSS staff member co-applicant is required"));
        }
        if (project.ProfessionalBackgroundIds.Count == 0)
        {
            result.Add(ProjectDiagnostic.EnrichmentWarning("Please populate professional background of all team/applicants"));
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // If submission deadline has passed and submission status N/A then red flag: has the project been submitted 

        if (project.FullSubmissionDeadline < today && project.FullSubmissionStatusId == FullSubmissionStatusEnum.NA)
        {
            result.Add(ProjectDiagnostic.EnrichmentWarning("Submission deadline has passed, has the project been submitted?"));
        }
        else if (project.OutlineSubmissionDeadline < today && project.OutlineSubmissionStatusId == OutlineSubmissionStatusEnum.NA)
        {
            result.Add(ProjectDiagnostic.EnrichmentWarning("Submission deadline has passed, has the project been submitted?"));
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // If submission yes but no outcome expected date then red flag: what is the outcome expected date

        if (project.FullSubmissionStatusId == FullSubmissionStatusEnum.Yes && project.FullOutcomeExpectedDate == null)
        {
            result.Add(ProjectDiagnostic.EnrichmentWarning("Project has been submitted, what is the outcome expected date?"));
        }
        else if (project.OutlineSubmissionStatusId == OutlineSubmissionStatusEnum.Yes && project.OutlineOutcomeExpectedDate == null)
        {
            result.Add(ProjectDiagnostic.EnrichmentWarning("Project has been submitted, What is the outcome expected date?"));
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // If expected deadline date has passed and outcome is Not Applicable then red flag: what was the project outcome

        if (project.FullOutcomeExpectedDate < today && project.FullOutcomeId == FullOutcomeEnum.NotApplicable)
        {
            result.Add(ProjectDiagnostic.EnrichmentWarning("Outcome expected date has passed, what is the outcome?"));
        }
        else if (project.OutlineOutcomeExpectedDate < today && project.OutlineOutcomeId == OutlineOutcomeEnum.NotApplicable)
        {
            result.Add(ProjectDiagnostic.EnrichmentWarning("Outcome expected date has passed, what is the outcome?"));
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Overdue Support Requests

        if (project.OverdueSupportRequestTeamIds.Count > 0)
        {
            result.Add(ProjectDiagnostic.EnrichmentWarning("Project has open support requests more than 2 weeks old"));
        }

        project.DiagnosticList = result;
        project.DiagnosticCount = result.Count;
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Heartbeat

    public async Task<BackgroundJobResult> ExecuteHeartbeat()
    {
        var project = await GetNextHeartbeatProject();

        if (project == null)
        {
            return new BackgroundJobResult(TimeSpan.FromHours(1));
        }

        var original = project.Copy();
        project.HeartbeatExecutedAt = DateTimeOffset.UtcNow;

        try
        {
            await CalculateSupportMetricsForProject(project);
            return new BackgroundJobResult("Executed heartbeat for project " + project.PrefixedNumber, TimeSpan.FromMinutes(2));
        }
        finally
        {
            await _projectRepository.UpdateProject(original: original, updated: project);
        }
    }

    async Task<Project?> GetNextHeartbeatProject()
    {
        var query = new Query();

        // Get active project with the oldest HeartbeatExecutedAt
        query.Equal(nameof(Project.Locked), false);
        query.Sort.Add(new QuerySort { Field = nameof(Project.HeartbeatExecutedAt), Direction = 1 });
        query.Skip = 0;
        query.Take = 1;

        var result = await _projectRepository.QueryProject(query, selectItems: true, countTotal: false);

        // We run the heartbeat at most once every 12 hours on a project
        if (result.Items.Count == 0 || result.Items[0].HeartbeatExecutedAt > DateTimeOffset.UtcNow.AddHours(-12))
            return null;

        return result.Items[0];
    }
}
