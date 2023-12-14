using System.Text;
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
            {
                var sb = new StringBuilder();
                foreach (var entity in entities)
                {
                    var value = await Accessor(entity);
                    if (value == null)
                        continue;

                    if (sb.Length > 0)
                        sb.Append(", ");

                    sb.Append(value.ToString());
                }
                return Task.FromResult<object?>(sb.ToString());
            }

            return await Accessor(entities[0]);
        }
    }
}
