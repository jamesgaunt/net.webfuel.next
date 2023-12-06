using System.Text.Json.Serialization;

namespace Webfuel.Reporting
{
    public class ReportPropertyField: ReportField
    {
        [JsonIgnore]
        public required Func<object, object?> Accessor { get; init; }

        public override Task<object?> Evaluate(object context, IServiceProvider services)
        {
            return Task.FromResult(Accessor(context));
        }
    }
}
