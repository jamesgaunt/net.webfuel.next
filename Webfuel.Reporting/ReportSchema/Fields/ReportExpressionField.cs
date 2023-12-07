using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    internal class ReportExpressionField : ReportField
    {
        public ReportExpressionField()
        {
            FieldType = ReportFieldType.Expression;
        }

        [JsonIgnore]
        public required string Expression { get; init; }

        public override Task<object?> Evaluate(object context, IServiceProvider services)
        {
            throw new NotImplementedException();
        }
    }
}
