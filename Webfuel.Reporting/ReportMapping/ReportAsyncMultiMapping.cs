using Microsoft.Extensions.DependencyInjection;
using System.Net.NetworkInformation;
using System.Text.Json.Serialization;

namespace Webfuel.Reporting
{
    internal class ReportAsyncMultiMapping<TEntity, TMapper> : IReportMapping 
        where TEntity : class
        where TMapper : class
    {
        public bool MultiValued => true;

        [JsonIgnore]
        public IReportMapping? ParentMapping { get; init; }

        [JsonIgnore]
        public required Func<object, TMapper, Task<List<Guid>>> Accessor { get; init; }

        public IReportMapper GetMapper(IServiceProvider services)
        {
            return (IReportMapper)services.GetRequiredService(typeof(IReportMapper<>).MakeGenericType(typeof(TEntity)));
        }

        public async Task<List<object>> MapContextsToEntities(List<object> contexts, ReportBuilder builder)
        {
            var seenIds = new HashSet<Guid>();
            var entities = new List<object>();

            if (ParentMapping != null)
                contexts = await ParentMapping.MapContextsToEntities(contexts, builder);

            var mapper = GetMapper(builder.ServiceProvider);

            foreach (var context in contexts)
            {
                var ids = await Accessor(context, builder.ServiceProvider.GetRequiredService<TMapper>());
                if (ids.Count == 0)
                    continue;

                foreach(var id in ids)
                {
                    // We only map a single entity once
                    if (seenIds.Contains(id))
                        continue;
                    seenIds.Add(id);

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
