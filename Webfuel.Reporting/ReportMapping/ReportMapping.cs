using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace Webfuel.Reporting
{

    internal class ReportMapping<TEntity> : IReportMapping 
        where TEntity : class 
    {
        public bool MultiValued => ParentMapping?.MultiValued ?? false;

        [JsonIgnore]
        public IReportMapping? ParentMapping { get; init; }

        [JsonIgnore]
        public required Func<object, Guid?> Accessor { get; init; }

        public IReportMapper GetMapper(IServiceProvider services)
        {
            return (IReportMapper)services.GetRequiredService(typeof(IReportMapper<>).MakeGenericType(typeof(TEntity)));
        }

        public async Task<List<object>> MapContextsToEntities(List<object> contexts, ReportBuilder builder)
        {
            var entities = new List<object>();

            if (ParentMapping != null)
                contexts = await ParentMapping.MapContextsToEntities(contexts, builder);

            var mapper = GetMapper(builder.ServiceProvider);

            foreach (var context in contexts)
            {
                var id = Accessor(context);
                if (id == null)
                    continue;

                var entity = await mapper.Get(id.Value);
                if (entity == null)
                    continue;

                entities.Add(entity);
            }

            return entities;
        }
    }
}
