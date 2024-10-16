namespace Webfuel.Domain;

public interface IWidgetDataProvider
{
    Task RefreshTask(WidgetRefreshTask task);

    Task<Widget> ValidateWidget(Widget widget);

    Task<Widget> UpdateConfig(Widget widget, string configJson);
}
