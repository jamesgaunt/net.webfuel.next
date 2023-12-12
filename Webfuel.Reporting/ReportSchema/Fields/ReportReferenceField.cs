using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace Webfuel.Reporting
{
    public interface IReportReferenceField
    {
        Task<ReportReference?> GetReference(Guid id, IServiceProvider services);

        Task<QueryResult<ReportReference>> QueryReference(Query query, IServiceProvider services);
    }

    public class ReportReferenceField<TEntity> : ReportField, IReportReferenceField where TEntity : class
    {
        protected override Task<object?> Evaluate(object context, ReportBuilder builder)
        {
            throw new NotImplementedException();
        }

        public async Task<ReportReference?> GetReference(Guid id, IServiceProvider services)
        {
            var referenceProvider = services.GetRequiredService<IReportReferenceProvider<TEntity>>();
            return await referenceProvider.Get(id);
        }

        public async Task<QueryResult<ReportReference>> QueryReference(Query query, IServiceProvider services)
        {
            var referenceProvider = services.GetRequiredService<IReportReferenceProvider<TEntity>>();
            return await referenceProvider.Query(query);
        }
    }
}
