using Azure.Core.Serialization;
using DocumentFormat.OpenXml.Drawing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Webfuel.Domain.StaticData;

namespace Webfuel.Domain;

public interface ITriageSummaryProvider: IWidgetProvider
{
}

[Service(typeof(ITriageSummaryProvider))]
internal class TriageSummaryProvider : ITriageSummaryProvider
{
    const int VERSION = 1;

    private readonly IWidgetRepository _widgetRepository;
    private readonly ISupportRequestRepository _supportRequestRepository;
    private readonly IServiceProvider _serviceProvider;

    public TriageSummaryProvider(
        IWidgetRepository widgetRepository,
        ISupportRequestRepository supportRequestRepository,
        IServiceProvider serviceProvider)
    {
        _widgetRepository = widgetRepository;
        _supportRequestRepository = supportRequestRepository;
        _serviceProvider = serviceProvider;
    }

    // Public API

    public Task<Widget> InitialiseWidget(Widget widget)
    {
        widget.HeaderText = "Triage Summary";
        widget.DataJson = SafeJsonSerializer.Serialize(new DashboardMetrics());
        return Task.FromResult(widget);
    }

    public async Task<WidgetTaskStatus> BeginProcessing(WidgetTask task)
    {
        if (task.Widget.DataVersion == VERSION && task.Widget.DataTimestamp > GlobalTimestamp)
            return WidgetTaskStatus.Complete;

        var data = await GenerateData();

        task.Widget.DataJson = SafeJsonSerializer.Serialize(data);
        task.Widget.DataVersion = VERSION;
        task.Widget.DataTimestamp = DateTimeOffset.UtcNow;
        task.Widget = await _widgetRepository.UpdateWidget(task.Widget);

        return WidgetTaskStatus.Complete;
    }

    public Task<WidgetTaskStatus> ContinueProcessing(WidgetTask task)
    {
        return Task.FromResult(WidgetTaskStatus.Complete);
    }

    public Task<bool> AuthoriseAccess()
    {
        var identityAccessor = _serviceProvider.GetRequiredService<IIdentityAccessor>();
        if (identityAccessor.User == null)
            return Task.FromResult(false);

        if (identityAccessor.Claims.Developer || identityAccessor.Claims.CanTriageSupportRequests)
            return Task.FromResult(true);

        return Task.FromResult(false);
    }

    public Task<string> ValidateConfig(string configJson)
    {
        return Task.FromResult(configJson);
    }

    // Generators (real time generation)

    async Task<DashboardMetrics> GenerateData()
    {
        var data = new DashboardMetrics
        {
            Metrics = await GenerateMetrics()
        };
        return data;
    }

    async Task<List<DashboardMetric>> GenerateMetrics()
    {
        var result = new List<DashboardMetric>();

        {
            var query = new Query();
            query.Equal(nameof(SupportRequest.StatusId), SupportRequestStatusEnum.ToBeTriaged);
            var queryResult = await _supportRequestRepository.QuerySupportRequest(query, selectItems: false, countTotal: true);

            result.Add(new DashboardMetric
            {
                Name = "To Be Triaged",
                Count = queryResult.TotalCount,
                Icon = "fas fa-books",
                RouterLink = "/support-request/support-request-list",
                RouterParams = $"{{ \"show\": \"to-be-triaged\" }}",
                BackgroundColor = "#d6bdcc"
            });
        }

        {
            var query = new Query();
            query.Equal(nameof(SupportRequest.StatusId), SupportRequestStatusEnum.OnHold);
            var queryResult = await _supportRequestRepository.QuerySupportRequest(query, selectItems: false, countTotal: true);

            result.Add(new DashboardMetric
            {
                Name = "On Hold",
                Count = queryResult.TotalCount,
                Icon = "fas fa-books",
                RouterLink = "/support-request/support-request-list",
                RouterParams = $"{{ \"show\": \"on-hold\" }}",
                BackgroundColor = "#d6bdcc"
            });
        }

        {
            var query = new Query();
            query.Equal(nameof(SupportRequest.StatusId), SupportRequestStatusEnum.ReferredToNIHRRSSExpertTeams);
            var queryResult = await _supportRequestRepository.QuerySupportRequest(query, selectItems: false, countTotal: true);

            result.Add(new DashboardMetric
            {
                Name = "Referred To NIHR RSS Expert Teams",
                Count = queryResult.TotalCount,
                Icon = "fas fa-books",
                RouterLink = "/support-request/support-request-list",
                RouterParams = $"{{ \"show\": \"referred-to-rss-team\" }}",
                BackgroundColor = "#d6bdcc"
            });
        }

        // All 
        {
            result.Add(new DashboardMetric
            {
                Name = "All",
                Count = await _supportRequestRepository.CountSupportRequest(),
                Icon = "fas fa-books",
                RouterLink = "/support-request/support-request-list",
                RouterParams = $"{{ \"show\": \"all\" }}",
                BackgroundColor = "#d6bdcc"

            });
        }

        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var query = new Query();
            query.All(x =>
            {
                x.GreaterThanOrEqual(nameof(SupportRequest.DateOfRequest), today.AddDays(-6), true);
                x.LessThanOrEqual(nameof(SupportRequest.DateOfRequest), today, true);
            });
            var queryResult = await _supportRequestRepository.QuerySupportRequest(query, selectItems: false, countTotal: true);

            result.Add(new DashboardMetric
            {
                Name = "Last 7 days",
                Count = queryResult.TotalCount,
                Icon = "fas fa-books",
                RouterLink = "/support-request/support-request-list",
                RouterParams = $"{{ \"show\": \"all\" }}",
                BackgroundColor = "#d6bdcc"
            });
        }

        return result;
    }

    public static void FlushSupportRequestMetrics()
    {
        GlobalTimestamp = DateTimeOffset.UtcNow;
    }

    static DateTimeOffset GlobalTimestamp = DateTimeOffset.UtcNow;
}
