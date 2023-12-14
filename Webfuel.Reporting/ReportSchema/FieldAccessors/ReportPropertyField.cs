using System.Text.Json.Serialization;

namespace Webfuel.Reporting
{
    public class ReportPropertyField: ReportField
    {
        [JsonIgnore]
        public required Func<object, object?> Accessor { get; init; }

        protected override Task<object?> GetValue(object context, ReportBuilder builder)
        {
            return Task.FromResult(Accessor(context));
        }
    }
}
