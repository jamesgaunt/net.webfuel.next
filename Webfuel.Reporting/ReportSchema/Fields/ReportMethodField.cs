using System.Text.Json.Serialization;

namespace Webfuel.Reporting
{
    public class ReportMethodField : ReportField
    {
        [JsonIgnore]
        public required Func<object, Task<object?>> Accessor { get; init; }

        public override Task<object?> Evaluate(object context, ReportBuilder builder)
        {
            return Accessor(context);
        }
    }
}
