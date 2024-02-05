namespace Webfuel.Reporting
{
    public interface IReportMapping
    {
        bool MultiValued { get; }

        Task<List<object>> MapContextsToEntities(List<object> contexts, ReportBuilder builder);

        IReportMapper GetMapper(IServiceProvider services);
    }
}
