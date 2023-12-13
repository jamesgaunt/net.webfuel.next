namespace Webfuel.Reporting
{
    internal interface IReportMapping
    {
        Task<object?> Map(object context, ReportBuilder builder);

        IReportMapper GetMapper(IServiceProvider services);

        bool MultiValued { get; }
    }
}
