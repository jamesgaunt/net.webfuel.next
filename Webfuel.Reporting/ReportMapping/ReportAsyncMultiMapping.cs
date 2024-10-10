using Microsoft.Extensions.DependencyInjection;
using System.Net.NetworkInformation;
using System.Text.Json.Serialization;

namespace Webfuel.Reporting
{
    internal class ReportAsyncMultiMapping<TEntity, TMap> : IReportMapping<TEntity, TMap>
        where TEntity : class
        where TMap : class, IReportMap<TEntity>
    {
        public bool MultiValued => true;

        [JsonIgnore]
        public Type MapType => typeof(TMap);

        [JsonIgnore]
        public IReportMapping? ParentMapping { get; init; }

        [JsonIgnore]
        public required Func<object, IServiceProvider, TMap, Task<List<Guid>>> Accessor { get; init; }

        public async Task<List<object>> MapContextsToEntities(List<object> contexts, ReportBuilder builder)
        {
            var seenIds = new HashSet<Guid>();
            var entities = new List<object>();

            if (ParentMapping != null)
                contexts = await ParentMapping.MapContextsToEntities(contexts, builder);

            var map = builder.ServiceProvider.GetRequiredService<TMap>();

            foreach (var context in contexts)
            {
                var ids = await Accessor(context, builder.ServiceProvider, map);
                if (ids.Count == 0)
                    continue;

                foreach(var id in ids)
                {
                    // We only map a single entity once
                    if (seenIds.Contains(id))
                        continue;
                    seenIds.Add(id);

                    var entity = await map.Get(id);
                    if (entity == null)
                        continue;

                    entities.Add(entity);
                }
            }

            return entities;
        }
    }
}
