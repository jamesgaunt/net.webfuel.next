namespace Webfuel.Reporting
{
    public interface IReportMapping
    {
        bool MultiValued { get; }

        Task<List<object>> MapContextsToEntities(List<object> contexts, ReportBuilder builder);

        Type MapType { get; }
    }

    public interface IReportMapping<TEntity, TMap> : IReportMapping
        where TEntity : class
        where TMap : class, IReportMap<TEntity>
    {
    }
}
