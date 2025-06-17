using Microsoft.Extensions.DependencyInjection;
using Webfuel.Domain.StaticData;
using Webfuel.Reporting;

namespace Webfuel.Domain;

public interface IProjectSupportReportProvider : IReportProvider
{
}

[Service(typeof(IProjectSupportReportProvider), typeof(IReportProvider))]
internal class ProjectSupportReportProvider : IProjectSupportReportProvider
{
    private readonly IProjectSupportRepository _projectSupportRepository;
    private readonly IServiceProvider _serviceProvider;

    public ProjectSupportReportProvider(IProjectSupportRepository projectSupportRepository, IServiceProvider serviceProvider)
    {
        _projectSupportRepository = projectSupportRepository;
        _serviceProvider = serviceProvider;
    }

    public Guid Id => ReportProviderEnum.ProjectSupport;

    public ReportBuilderBase GetReportBuilder(ReportRequest request)
    {
        return new ReportBuilder(request);
    }

    public async Task<IEnumerable<object>> QueryItems(Query query)
    {
        var result = await _projectSupportRepository.QueryProjectSupport(query, countTotal: false);
        return result.Items;
    }

    public async Task<int> GetTotalCount(Query query)
    {
        var result = await _projectSupportRepository.QueryProjectSupport(query, selectItems: false, countTotal: true);
        return result.TotalCount;
    }

    public ReportSchema Schema
    {
        get
        {
            if (_schema == null)
            {
                var builder = ReportSchemaBuilder<ProjectSupport>.Create(ReportProviderEnum.ProjectSupport);

                builder.Add(Guid.Parse("25c60e05-6514-4969-b844-e984326f4f36"), "Date of Support", p => p.Date);

                builder.Add(Guid.Parse("57204248-4827-4f69-a739-14f1c70dd617"), "Time in Hours per Adviser", p => p.WorkTimeInHours);

                builder.Add(Guid.Parse("1ccde90c-b368-4686-90a1-85ec63e35d52"), "Time in Hours Total", p => p.WorkTimeInHours * p.AdviserIds.Count);

                builder.Map<IsPrePostAward>(Guid.Parse("b134afbf-e2b8-4de3-b614-51a4abfd6ef4"), "Pre/Post Award", p => p.IsPrePostAwardId);

                builder.Map<Project>(Guid.Parse("609b9d79-4ff5-46af-9ff7-19099d879bb8"), "Project", async (p, s, m) => (await s.GetRequiredService<IProjectRepository>().GetProjectByProjectSupportGroupId(p.ProjectSupportGroupId))?.Id, a =>
                {
                    a.Add(Guid.Parse("b9dc13e6-c221-4886-8243-7618d5fdffbf"), "Project Number", p => p.Number);
                    a.Add(Guid.Parse("810b1424-5d44-40d4-b84f-438636a1c0e0"), "Project Title", p => p.Title);
                    a.Add(Guid.Parse("3d18e615-0e65-4492-bdb3-6ff05ab6030a"), "Project Date of Request", p => p.DateOfRequest);

                    a.Map<ProjectStatus>(Guid.Parse("0bfd322c-e155-410f-937a-52c547d1949f"), "Project Status", p => p.StatusId);
                    a.Map<User>(Guid.Parse("7820e23c-9b11-4539-8477-552814e351da"), "Project Lead Adviser", p => p.LeadAdviserUserId);
                });

                builder.Map<User>(Guid.Parse("a542588f-4ba3-418b-8957-0040155f9287"), "Advisers", p => p.AdviserIds);
                builder.Map<SupportTeam>(Guid.Parse("37fa0df6-9ef3-4b3b-9f66-7420b655833b"), "Support Teams", p => p.TeamIds);
                builder.Map<SupportProvided>(Guid.Parse("2ccd3c99-9251-4051-8adc-a1970ac81511"), "Support Provided", p => p.SupportProvidedIds);

                builder.Map<SupportTeam>(Guid.Parse("fb508f1b-1813-4efe-bf79-b466e2007a0d"), "Request Support From Another Team", p => p.SupportRequestedTeamId);
                builder.Add(Guid.Parse("50ca734f-37e3-490c-b882-d6f1524d6a25"), "Support Requested At", p => p.SupportRequestedAt);
                builder.Add(Guid.Parse("8831278d-b720-4336-9354-b5a9b5240588"), "Support Request Completed", p => p.SupportRequestedCompletedAt);
                builder.Add(Guid.Parse("75c47039-14b6-4d4a-88e2-13f1b27419d5"), "Support Request Completed Notes", p => p.SupportRequestedCompletedNotes);

                builder.Add(Guid.Parse("ece243c3-b5dd-4c25-a559-5e4ffe2bafe0"), "Description", p => p.Description);

                _schema = builder.Schema;
            }

            return _schema;
        }
    }

    static ReportSchema? _schema = null;
}
