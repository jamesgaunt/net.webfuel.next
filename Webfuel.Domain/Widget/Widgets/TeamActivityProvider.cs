using DocumentFormat.OpenXml.Office2010.Drawing;
using DocumentFormat.OpenXml.Presentation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using System.Text.Json;
using Webfuel.Domain.StaticData;
using Webfuel.Reporting;
using Webfuel.Terminal;

namespace Webfuel.Domain;

[ApiType]
public class TeamActivityData
{
    public List<TeamMember> TeamMembers { get; set; } = new List<TeamMember>();

    public class TeamMember
    {
        public string Name { get; set; } = String.Empty;

        public decimal ProjectSupportHours { get; set; }

        public decimal UserActivityHours { get; set; }

        public decimal TotalHours { get; set; }

        public decimal? FullTimeEquivalent { get; set; }
    }
}

[ApiType]
public class TeamActivityConfig
{
    public Guid SupportTeamId { get; set; } = SupportTeamEnum.TriageTeam;
}

public interface ITeamActivityProvider : IWidgetProvider
{
}

[Service(typeof(ITeamActivityProvider))]
internal class TeamActivityProvider : ITeamActivityProvider
{
    const int VERSION = 1;

    private readonly IMediator _mediator;
    private readonly IReportService _reportService;
    private readonly IWidgetRepository _widgetRepository;
    private readonly IReportGeneratorService _reportGeneratorService;
    private readonly IStaticDataService _staticDataService;
    private readonly IServiceProvider _serviceProvider;

    public TeamActivityProvider(
        IMediator mediator,
        IWidgetRepository widgetRepository,
        IReportService reportService,
        IReportGeneratorService reportGeneratorService,
        IStaticDataService staticDataService,
        IServiceProvider serviceProvider)
    {
        _mediator = mediator;
        _reportService = reportService;
        _widgetRepository = widgetRepository;
        _reportGeneratorService = reportGeneratorService;
        _staticDataService = staticDataService;
        _serviceProvider = serviceProvider;
    }

    // Public API

    public Task<Widget> Initialise(Widget widget)
    {
        widget.HeaderText = "Team Activity";
        widget.DataJson = SafeJsonSerializer.Serialize(new TeamActivityData());
        widget.ConfigJson = SafeJsonSerializer.Serialize(new TeamActivityConfig());
        return Task.FromResult(widget);
    }

    public async Task<WidgetTaskStatus> ProcessTask(WidgetTask task)
    {
        if (task.Widget.DataVersion == VERSION && task.Widget.DataTimestamp > GlobalTimestamp)
            return WidgetTaskStatus.Complete;

        return await RunReport(task);
    }

    public async Task<bool> AuthoriseAccess()
    {
        var identityAccessor = _serviceProvider.GetRequiredService<IIdentityAccessor>();
        if (identityAccessor.User == null)
            return false;

        if (identityAccessor.Claims.Developer)
            return true;

        var supportTeams = await _serviceProvider.GetRequiredService<ISupportTeamUserRepository>().SelectSupportTeamUserByUserId(identityAccessor.User.Id);
        if (supportTeams.Any(p => p.IsTeamLead))
            return true;

        return false;
    }

    // Implementation

    async Task<WidgetTaskStatus> RunReport(WidgetTask task)
    {
        var sDate = DateOnly.FromDateTime(DateTime.Today).AddDays(-14);
        var eDate = DateOnly.FromDateTime(DateTime.Today);

        if (task.State is not ReportStep reportStep)
        {
            var config = SafeJsonSerializer.Deserialize<TeamActivityConfig>(task.Widget.ConfigJson);
            var report = await _reportService.GetDefaultNamedReport("Team Activity Report", ReportProviderEnum.CustomReport);

            var runReport = new RunReport
            {
                ReportId = report.Id,
                TypedArguments = new TeamActivityReportArguments
                {
                    SupportTeamId = config.SupportTeamId,
                    StartDate = sDate,
                    EndDate = eDate
                }
            };

            task.State = await _mediator.Send(runReport);
            return WidgetTaskStatus.Processing;
        }

        // Generate the Report Task

        task.State = reportStep = await _reportGeneratorService.GenerateReport(reportStep.TaskId);
        if (!reportStep.Complete)
            return WidgetTaskStatus.Processing;

        if (_reportGeneratorService.ExtractReportData(reportStep.TaskId) is not TeamActivityReportData reportData)
            throw new InvalidOperationException("Report did not return the expected data type");

        // We have the final report data
        {
            var config = SafeJsonSerializer.Deserialize<TeamActivityConfig>(task.Widget.ConfigJson);

            task.Widget.DataJson = SafeJsonSerializer.Serialize(MapReportData(reportData));
            task.Widget.DataVersion = VERSION;
            task.Widget.DataTimestamp = DateTimeOffset.UtcNow;

            var supportTeam = await _staticDataService.GetSupportTeam(config.SupportTeamId);

            task.Widget.HeaderText = (supportTeam?.Name ?? "Invalid Team Id");
            task.Widget.HeaderText += $" {sDate.ToString("dd/MM/yy")} - {eDate.ToString("dd/MM/yy")}";

            task.Widget = await _widgetRepository.UpdateWidget(task.Widget);

            return WidgetTaskStatus.Complete;
        }
    }

    TeamActivityData MapReportData(TeamActivityReportData reportData)
    {
        // Map data from the report into data for the widget

        var data = new TeamActivityData();
        foreach (var user in reportData.Users)
        {
            data.TeamMembers.Add(new TeamActivityData.TeamMember
            {
                Name = user.User.FullName,
                ProjectSupportHours = user.ProjectSupportHours,
                UserActivityHours = user.UserActivityHours,
                TotalHours = user.ProjectSupportHours + user.UserActivityHours,
                FullTimeEquivalent = user.User.FullTimeEquivalentForRSS,
            });
        }
        return data;
    }

    // Cache

    public static void FlushTeamActivityMetrics()
    {
        GlobalTimestamp = DateTimeOffset.UtcNow;
    }

    static DateTimeOffset GlobalTimestamp = DateTimeOffset.UtcNow;
}
