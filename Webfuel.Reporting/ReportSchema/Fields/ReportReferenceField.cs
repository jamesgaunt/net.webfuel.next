using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace Webfuel.Reporting
{
    public class ReportReferenceField : ReportField
    { 
        protected override Task<object?> Evaluate(object context, ReportBuilder builder)
        {
            var mapper = GetMapper(builder.ServiceProvider);
            return Task.FromResult<object?>(mapper.DisplayName(context));
        }

    }
}
