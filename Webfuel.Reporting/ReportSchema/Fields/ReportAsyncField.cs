using System.Text.Json.Serialization;

namespace Webfuel.Reporting
{
    public class ReportAsyncField : ReportField
    {
        [JsonIgnore]
        public required Func<object, Task<object?>> Accessor { get; init; }

        protected override async Task<object?> MapEntitiesToValue(List<object> entities, ReportBuilder builder)
        {
            if (entities.Count == 0)
                return null;

            if (entities.Count > 1)
                return "MULTI-VALUE NOT SUPPORTED";

            return await Accessor(entities[0]);
        }
    }
}
