using DocumentFormat.OpenXml.Spreadsheet;
using System.Text;
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
            {
                var sb = new StringBuilder();
                foreach (var entity in entities)
                {
                    var value = Accessor(entity);
                    if (value == null)
                        continue;

                    if (sb.Length > 0)
                        sb.Append(", ");

                    sb.Append(value.ToString());
                }
                return Task.FromResult<object?>(sb.ToString());
            }

            return Task.FromResult(Accessor(entities[0]));
        }
    }
}
