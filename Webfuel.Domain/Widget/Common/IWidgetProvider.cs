namespace Webfuel.Domain;

public interface IWidgetProvider
{
    Task<Widget> InitialiseWidget(Widget widget);

    Task<WidgetTaskStatus> BeginProcessing(WidgetTask task);

    Task<WidgetTaskStatus> ContinueProcessing(WidgetTask task);

    Task<bool> AuthoriseAccess();

    Task<string> ValidateConfig(string config);
}
