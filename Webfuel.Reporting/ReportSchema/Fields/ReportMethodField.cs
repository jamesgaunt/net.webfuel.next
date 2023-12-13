using System.Text.Json.Serialization;

namespace Webfuel.Reporting
{
    public class ReportMethodField : ReportField
    {
        [JsonIgnore]
        public required Func<object, Task<object?>> Accessor { get; init; }

        protected override async Task<object?> EvaluateImpl(object context, ReportBuilder builder)
        {
            return await Accessor(context);
        }
    }
}
