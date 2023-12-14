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
        public required Guid Id { get; init; }

        public required String Name { get; init; }

        [JsonIgnore]
        internal IReportMapping? Mapping { get; init; }

        public required ReportFieldType FieldType { get; init; }

        protected abstract Task<object?> GetValue(object context, ReportBuilder builder);

        internal async Task<object?> Map(object context, ReportBuilder builder)
        {
            if (Mapping != null)
                return await Mapping.Map(context, builder);
            return context;
        }

        internal async Task<object?> Evaluate(object context, ReportBuilder builder)
        {
            var mapped = await Map(context, builder);
            if (mapped == null)
                return null;
            return await GetValue(mapped, builder);
        }

        public IReportMapper GetMapper(IServiceProvider services)
        {
            if (Mapping == null)
                throw new InvalidOperationException($"Field {Name} is not a mapped field.");
            return Mapping.GetMapper(services);
        }
    }
}
