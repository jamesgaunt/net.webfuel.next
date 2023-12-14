using DocumentFormat.OpenXml.Spreadsheet;
using System.Text.Json.Serialization;

namespace Webfuel.Reporting
{
    public class ReportPropertyField: ReportField
    {
        [JsonIgnore]
        public required Func<object, object?> Accessor { get; init; }

        protected override Task<object?> MapEntitiesToValue(List<object> entities, ReportBuilder builder)
        {
            if (entities.Count == 0)
                return Task.FromResult<object?>(null);

            if (entities.Count > 1)
                return Task.FromResult<object?>("MULTI-VALUE NOT SUPPORTED");

            return Task.FromResult(Accessor(entities[0]));
        }
    }
}
