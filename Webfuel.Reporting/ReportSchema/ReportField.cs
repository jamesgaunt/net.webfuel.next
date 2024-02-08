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

        internal async Task<object?> Evaluate(object context, ReportBuilder builder)
        {
            var entities = await MapContextToEntities(context, builder);
            var values = await MapEntitiesToValues(entities, builder);

            return CollectValues(values);
        }

        internal async Task<List<object>> MapContextToEntities(object context, ReportBuilder builder)
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

        object? CollectValues(List<object> values)
        {
            if (values.Count == 0)
                return null;

            if(values.Count == 1)
                return values[0];

            if(FieldType == ReportFieldType.Number)
            {
                var sum = 0D;
                foreach (var value in values)
                {
                    if (IsNumericType(value.GetType()))
                        sum += Convert.ToDouble(value);
                    else
                        throw new InvalidOperationException("The field is of type number but the value is not numeric");
                }
                return sum;
            }

            return String.Join(", ", values);
        }

        static bool IsNumericType(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }
    }
}
