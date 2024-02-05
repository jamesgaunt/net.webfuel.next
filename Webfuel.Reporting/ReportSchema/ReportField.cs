using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    [ApiType]
    public abstract class ReportField
    {
        // Server + Client Side

        public required Guid Id { get; init; }

        public required String Name { get; init; }

        public required ReportFieldType FieldType { get; init; }

        public bool Filterable { get; init; } = true;

        // Server Side

        [JsonIgnore]
        internal IReportMapping? Mapping { get; init; }

        internal async Task<object?> Evaluate(object context, ReportBuilder builder)
        {
            var entities = await MapContextToEntities(context, builder);
            return await MapEntitiesToValue(entities, builder);
        }

        internal async Task<List<object>> MapContextToEntities(object context, ReportBuilder builder)
        {
            return Mapping == null ?
                new List<object> { context } :
                await Mapping.MapContextsToEntities(new List<object> { context }, builder);
        }

        public IReportMapper GetMapper(IServiceProvider services)
        {
            if (Mapping == null)
                throw new InvalidOperationException($"Field {Name} is not a mapped field.");
            return Mapping.GetMapper(services);
        }

        protected abstract Task<object?> MapEntitiesToValue(List<object> entities, ReportBuilder builder);
    }
}
