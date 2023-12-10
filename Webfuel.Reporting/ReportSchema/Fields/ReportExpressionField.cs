using System.Text.Json.Serialization;

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
