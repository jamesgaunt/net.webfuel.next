using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace Webfuel.Reporting
{
    public class ReportReferenceField : ReportField
    {
        [JsonIgnore]
        public required Func<object, Guid?> Accessor { get; init; }

        [JsonIgnore]
        public required Type ReferenceProviderType { get; init; }

        public override async Task<object?> Evaluate(object context, IServiceProvider services)
        {
            var id = Accessor(context);
            if (id == null)
                return null;

            var referenceProvider = (IReportReferenceProvider)services.GetRequiredService(ReferenceProviderType);

            var reference = await referenceProvider.GetReportReference(id.Value);
            if(reference == null)
                return null;

            return reference.Name;
        }
    }
}
