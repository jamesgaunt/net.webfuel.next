using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    [ApiType]
    public class ReportFilterExpression : ReportFilter
    {
        public override ReportFilterType FilterType => ReportFilterType.Expression;

        public override IEnumerable<ReportFilterCondition> Conditions => Enumerable.Empty<ReportFilterCondition>();

        public override string DisplayName => "Expression";

        public string Expression { get; set; } = String.Empty;

        public override async Task<bool> Validate(ReportSchema schema, IServiceProvider services)
        {
            if (!await base.Validate(schema, services))
                return false;

            Description = String.IsNullOrWhiteSpace(Expression) ? "(empty expression)" : Expression;
            return true;
        }

        public override Task<bool> Apply(object context, ReportBuilder builder)
        {
            throw new NotImplementedException();
        }

        public override void Update(ReportFilter filter, ReportSchema schema)
        {
            if (filter is not ReportFilterExpression typed)
                throw new Exception($"Cannot apply filter of type {filter.FilterType} to filter of type {FilterType}");

            Expression = typed.Expression;
            base.Update(filter, schema);
        }

        // Serialization

        public override bool ReadProperty(string propertyName, ref Utf8JsonReader reader)
        {
            if (String.Compare(nameof(Expression), propertyName, true) == 0)
            {
                Expression = reader.GetString() ?? String.Empty;
                return reader.Read();
            }

            return base.ReadProperty(propertyName, ref reader);
        }

        public override void WriteProperties(Utf8JsonWriter writer)
        {
            base.WriteProperties(writer);
            writer.WriteString("expression", Expression);
        }
    }
}
