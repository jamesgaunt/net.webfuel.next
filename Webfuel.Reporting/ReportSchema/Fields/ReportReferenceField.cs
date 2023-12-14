using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Text.Json.Serialization;

namespace Webfuel.Reporting
{
    public class ReportReferenceField : ReportField
    {
        protected override Task<object?> MapEntitiesToValue(List<object> entities, ReportBuilder builder)
        {
            var sb = new StringBuilder();
            foreach (var entity in entities)
            {
                if (sb.Length > 0)
                    sb.Append(", ");

                sb.Append(GetMapper(builder.ServiceProvider).DisplayName(entity));
            }
            return Task.FromResult<object?>(sb.ToString());
        }
    }
}
