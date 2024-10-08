using Azure.Core.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Webfuel.Domain.StaticData;

namespace Webfuel.Domain;

public class ProjectSummaryData
{
    public bool Cached { get; set; }

    public List<DashboardMetric> ProjectMetrics { get; set; } = new List<DashboardMetric>();
}

public interface IProjectSummaryProvider
{
    Task<ProjectSummaryData> GetData(Guid widgetId);
}

[Service(typeof(IProjectSummaryProvider))]
internal class ProjectSummaryProvider : IProjectSummaryProvider
{
    const int VERSION = 1;

    private readonly IWidgetRepository _widgetRepository;
    private readonly IProjectRepository _projectRepository;

    public ProjectSummaryProvider(
        IWidgetRepository widgetRepository, IProjectRepository projectRepository)
    {
        _widgetRepository = widgetRepository;
        _projectRepository = projectRepository;
    }

    public async Task<ProjectSummaryData> GetData(Guid widgetId)
    {
        var data = new ProjectSummaryData();

        var widget = await _widgetRepository.GetWidget(widgetId);
        if (widget == null)
            return data;

        if (widget.CachedDataVersion == VERSION && widget.CachedDataTimestamp > GlobalTimestamp)
        {
            try
            {
                data = JsonSerializer.Deserialize<ProjectSummaryData>(widget.CachedData);
                if (data != null)
                {
                    data.Cached = true;
                    return data;
                }
            }
            catch { /* GULP */ }
        }

        data = await GenerateData();

        widget.CachedData = JsonSerializer.Serialize(data);
        widget.CachedDataVersion = VERSION;
        widget.CachedDataTimestamp = DateTimeOffset.UtcNow;

        await _widgetRepository.UpdateWidget(widget);

        return data;
    }

    // Generators (real time generation)

    async Task<ProjectSummaryData> GenerateData()
    {
        var data = new ProjectSummaryData
        {
            ProjectMetrics = await GenerateProjectMetrics()
        };
        return data;
    }

    async Task<List<DashboardMetric>> GenerateProjectMetrics()
    {
        var result = new List<DashboardMetric>();

        {
            var query = new Query();
            query.Equal(nameof(Project.StatusId), ProjectStatusEnum.Active);
            var queryResult = await _projectRepository.QueryProject(query, selectItems: false, countTotal: true);

            result.Add(new DashboardMetric
            {
                Name = "Active Projects",
                Count = queryResult.TotalCount,
                Icon = "fas fa-books",
                RouterLink = "/project/project-list",
                RouterParams = $"{{ \"show\": \"active\" }}",
                BackgroundColor = "#d6bdcc"
            });
        }

        {
            var query = new Query();
            query.Equal(nameof(Project.StatusId), ProjectStatusEnum.OnHold);
            var queryResult = await _projectRepository.QueryProject(query, selectItems: false, countTotal: true);

            result.Add(new DashboardMetric
            {
                Name = "On Hold Projects",
                Count = queryResult.TotalCount,
                Icon = "fas fa-books",
                RouterLink = "/project/project-list",
                RouterParams = $"{{ \"show\": \"on-hold\" }}",
                BackgroundColor = "#d6bdcc"
            });
        }

        {
            var query = new Query();
            query.Equal(nameof(Project.StatusId), ProjectStatusEnum.SubmittedOnHold);
            var queryResult = await _projectRepository.QueryProject(query, selectItems: false, countTotal: true);

            result.Add(new DashboardMetric
            {
                Name = "Submitted - On Hold Projects",
                Count = queryResult.TotalCount,
                Icon = "fas fa-books",
                RouterLink = "/project/project-list",
                RouterParams = $"{{ \"show\": \"submitted-on-hold\" }}",
                BackgroundColor = "#d6bdcc"
            });
        }

        // All Projects
        {
            result.Add(new DashboardMetric
            {
                Name = "All Projects",
                Count = await _projectRepository.CountProject(),
                Icon = "fas fa-books",
                RouterLink = "/project/project-list",
                RouterParams = $"{{ \"show\": \"all\" }}",
                BackgroundColor = "#d6bdcc"

            });
        }

        return result;
    }

    public static void FlushProjectMetrics()
    {
        GlobalTimestamp = DateTimeOffset.UtcNow;
    }

    static DateTimeOffset GlobalTimestamp = DateTimeOffset.UtcNow;
}
