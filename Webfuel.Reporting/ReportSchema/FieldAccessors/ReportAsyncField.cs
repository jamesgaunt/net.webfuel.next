using System.Text.Json.Serialization;

namespace Webfuel.Reporting
{
    public class ReportAsyncField : ReportField
    {
        [JsonIgnore]
        public required Func<object, Task<object?>> Accessor { get; init; }

        protected override async Task<object?> GetValue(object context, ReportBuilder builder)
        {
            return await Accessor(context);
        }
    }
}
