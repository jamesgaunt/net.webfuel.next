using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Text.Json.Serialization;

namespace Webfuel.Reporting
{
    public class ReportReferenceListField : ReportField
    {
        [JsonIgnore]
        public required Func<object, IEnumerable<Guid>> Accessor { get; init; }

        [JsonIgnore]
        public required Type ReferenceProviderType { get; init; }

        public override async Task<object?> Evaluate(object context, ReportBuilder builder)
        {
            var ids = Accessor(context);
            if (ids == null)
                return null;

            var referenceProvider = (IReportReferenceProvider)builder.ServiceProvider.GetRequiredService(ReferenceProviderType);
            var result = new StringBuilder();

            foreach(var id in ids)
            {
                var reference = await referenceProvider.GetReportReference(id);
                if (reference == null)
                    continue;

                if (result.Length > 0)
                    result.Append(", ");

                result.Append(reference.Name);
            }

            return result.ToString();
        }
    }
}
