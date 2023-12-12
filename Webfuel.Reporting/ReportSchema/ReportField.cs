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

        protected abstract Task<object?> Evaluate(object context, ReportBuilder builder);

        internal async Task<object?> Extract(object context, ReportBuilder builder)
        {
            if (Mapping != null)
            {
                var temp = await Mapping.Map(context, builder);
                if (temp == null)
                    return null;

                // If this is a reference field we can short circuit the evaluation
                if (this is IReportReferenceField referenceField)
                    return temp;

                context = temp.Entity;
            }

            return await Evaluate(context, builder);
        }
    }
}
