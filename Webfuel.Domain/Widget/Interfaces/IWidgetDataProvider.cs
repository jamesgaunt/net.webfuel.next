namespace Webfuel.Domain;

public interface IWidgetDataProvider
{
    Task<WidgetDataResponse> GenerateData(WidgetDataTask task);
}
