using DocumentFormat.OpenXml.Presentation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using System.Text.Json;
using Webfuel.Domain.StaticData;
using Webfuel.Reporting;

namespace Webfuel.Domain;

[ApiType]
public class TeamActivityData
{
    public List<TeamMember> TeamMembers { get; set; } = new List<TeamMember>();

    public class TeamMember
    {
        public required string Name { get; set; }

        public required decimal ProjectSupportHours { get; set; }

        public required decimal UserActivityHours { get; set; }

        public required decimal TotalHours { get; set; }
    }
}

public interface ITeamActivityProvider : IWidgetDataProvider
{
}

[Service(typeof(ITeamActivityProvider))]
internal class TeamActivityProvider : ITeamActivityProvider
{
    const int VERSION = 1;

    private readonly IMediator _mediator;
    private readonly IWidgetRepository _widgetRepository;
    private readonly IReportGeneratorService _reportGeneratorService;
    private readonly IServiceProvider _serviceProvider;

    public TeamActivityProvider(
        IMediator mediator,
        IWidgetRepository widgetRepository,
        IReportGeneratorService reportGeneratorService,
        IServiceProvider serviceProvider)
    {
        _mediator = mediator;
        _widgetRepository = widgetRepository;
        _reportGeneratorService = reportGeneratorService;
        _serviceProvider = serviceProvider;
    }

    public async Task<WidgetDataResponse> GenerateData(WidgetDataTask task)
    {
        var widget = task.Widget;
        if (widget.CachedDataVersion == VERSION && widget.CachedDataTimestamp > GlobalTimestamp)
            return new WidgetDataResponse { Complete = true, Data = widget.CachedData };

        var dataResponse = await RunReport(task);
        if (dataResponse.Complete == false)
            return dataResponse;

        widget.CachedData = dataResponse.Data;
        widget.CachedDataVersion = VERSION;
        widget.CachedDataTimestamp = DateTimeOffset.UtcNow;

        await _widgetRepository.UpdateWidget(widget);

        return new WidgetDataResponse { Complete = true, Data = widget.CachedData };
    }

    // Generators (real time generation)

    async Task<WidgetDataResponse> RunReport(WidgetDataTask task)
    {
        if(task.GeneratorState is not ReportStep reportStep)
        {
            // Initialise the Report Task

            var report = await _serviceProvider.GetRequiredService<IReportService>().GetDefaultNamedReport("Team Activity Report", ReportProviderEnum.CustomReport);

            // Time to initialise the report
            var runReport = new RunReport
            {
                ReportId = report.Id,
                TypedArguments = new TeamActivityReportArguments
                {
                    StartDate = DateOnly.FromDateTime(DateTime.Today).AddMonths(-1),
                    EndDate = DateOnly.FromDateTime(DateTime.Today)
                }
            };

            task.GeneratorState = await _mediator.Send(runReport);
            return new WidgetDataResponse { Complete = false };
        }

        // Generate the Report Task

        task.GeneratorState = reportStep = await _reportGeneratorService.GenerateReport(reportStep.TaskId);

        if (reportStep.Complete)
        {
            if (_reportGeneratorService.ExtractReportData(reportStep.TaskId) is not TeamActivityReportData reportData)
                throw new InvalidOperationException("Report did not return the expected data type");

            // We have the final report data

            return new WidgetDataResponse
            {
                Complete = true,
                Data = JsonSerializer.Serialize(MapReportData(reportData), new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase })
            };
        }

        return new WidgetDataResponse { Complete = false };
    }

    TeamActivityData MapReportData(TeamActivityReportData reportData)
    {
        // Map data from the report into data for the widget

        var data = new TeamActivityData();
        foreach(var user in reportData.Users)
        {
            data.TeamMembers.Add(new TeamActivityData.TeamMember
            {
                Name = user.User.FullName,
                ProjectSupportHours = user.ProjectSupportHours,
                UserActivityHours = user.UserActivityHours,
                TotalHours = user.ProjectSupportHours + user.UserActivityHours
            });
        }
        return data;
    }

    public static void FlushTeamActivityMetrics()
    {
        GlobalTimestamp = DateTimeOffset.UtcNow;
    }

    static DateTimeOffset GlobalTimestamp = DateTimeOffset.UtcNow;
}
