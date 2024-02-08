using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace Webfuel.Reporting
{

    internal class ReportMapping<TEntity, TMap> : IReportMapping<TEntity, TMap>
        where TEntity : class 
        where TMap : class, IReportMap<TEntity>
    {
        public bool MultiValued => ParentMapping?.MultiValued ?? false;

        [JsonIgnore]
        public Type MapType => typeof(TMap);

        [JsonIgnore]
        public IReportMapping? ParentMapping { get; init; }

        [JsonIgnore]
        public required Func<object, Guid?> Accessor { get; init; }

        public async Task<List<object>> MapContextsToEntities(List<object> contexts, ReportBuilder builder)
        {
            var entities = new List<object>();

            if (ParentMapping != null)
                contexts = await ParentMapping.MapContextsToEntities(contexts, builder);

            var map = builder.ServiceProvider.GetRequiredService<TMap>();

            foreach (var context in contexts)
            {
                var id = Accessor(context);
                if (id == null)
                    continue;

                var entity = await map.Get(id.Value);
                if (entity == null)
                    continue;

                entities.Add(entity);
            }

            return entities;
        }
    }
}
