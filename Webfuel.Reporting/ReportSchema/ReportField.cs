using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    [ApiType]
    public class ReportField
    {
        // Server + Client Side

        public required Guid Id { get; init; }

        public required String Name { get; init; }

        public required ReportFieldType FieldType { get; init; }

        public required bool MultiValued { get; init; }

        public required bool Filterable { get; init; }

        // Server Side

        [JsonIgnore]
        internal IReportMapping? Mapping { get; init; }

        [JsonIgnore]
        public IReportAccessor? Accessor { get; init; }

        internal async ValueTask<List<object>> GetValues(object context, ReportBuilder builder)
        {
            var entities = await MapContextToEntities(context, builder);
            return await MapEntitiesToValues(entities, builder);
        }

        internal async ValueTask<object?> GetValue(object context, ReportBuilder builder)
        {
            var values = await GetValues(context, builder);

            if (values.Count == 0)
                return null;

            if (values.Count == 1)
                return values[0];

            if (FieldType == ReportFieldType.Number)
                return ReportBuilderCollect.Sum(values);
            return ReportBuilderCollect.List(values);
        }

        internal async ValueTask<List<object>> MapContextToEntities(object context, ReportBuilder builder)
        {
            return Mapping == null ?
                new List<object> { context } :
                await Mapping.MapContextsToEntities(new List<object> { context }, builder);
        }

        internal IReportMap GetMap(IServiceProvider serviceProvider)
        {
            if (Mapping == null)
                throw new InvalidOperationException("The specified field is not mapped");
            return (IReportMap)serviceProvider.GetRequiredService(Mapping.MapType);
        }

        async ValueTask<List<object>> MapEntitiesToValues(List<object> entities, ReportBuilder builder)
        {
            var result = new List<object>();

            if (entities.Count == 0 || Accessor == null)
                return result;

            foreach (var entity in entities)
            {
                var value = await Accessor.GetValue(entity, builder);
                if (value == null)
                    continue;

                result.Add(value);
            }
            return result;
        }
    }
}
