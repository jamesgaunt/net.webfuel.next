namespace Webfuel.Domain;

public interface IWidgetProvider
{
    Task<Widget> Initialise(Widget widget);

    Task<WidgetTaskStatus> ProcessTask(WidgetTask task);

    Task<bool> AuthoriseAccess();
}
