using Microsoft.Extensions.DependencyInjection;
using System.Net.NetworkInformation;
using System.Text.Json.Serialization;

namespace Webfuel.Reporting
{
    internal class ReportMultiMapping<TEntity> : IReportMapping 
        where TEntity : class 
    {
        public bool MultiValued => true;

        [JsonIgnore]
        public IReportMapping? ParentMapping { get; init; }

        [JsonIgnore]
        public required Func<object, List<Guid>> Accessor { get; init; }

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
                var ids = Accessor(context);
                if (ids.Count == 0)
                    continue;

                foreach(var id in ids)
                {
                    var entity = await mapper.Get(id);
                    if (entity == null)
                        continue;
                    entities.Add(entity);
                }
            }

            return entities;
        }
    }
}
