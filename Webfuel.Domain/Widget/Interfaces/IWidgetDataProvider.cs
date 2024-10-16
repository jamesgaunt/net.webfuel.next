namespace Webfuel.Domain;

public interface IWidgetDataProvider
{
    Task ValidateWidget(Widget widget);

    Task RefreshWidget(WidgetRefreshTask task);
}
