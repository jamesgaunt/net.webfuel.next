using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    public interface IReportAccessor
    {
        ValueTask<object?> GetValue(object entity, ReportBuilder builder);
    }

    public class ReportPropertyAccessor: IReportAccessor
    {
        [JsonIgnore]
        public required Func<object, object?> Accessor { get; init; }

        public ValueTask<object?> GetValue(object entity, ReportBuilder builder)
        {
            return new ValueTask<object?>(Accessor(entity));
        }
    }

    public class ReportAsyncAccessor: IReportAccessor
    {
        [JsonIgnore]
        public required Func<object, IServiceProvider, Task<object?>> Accessor { get; init; }

        public async ValueTask<object?> GetValue(object entity, ReportBuilder builder)
        {
            return await Accessor(entity, builder.ServiceProvider);
        }
    }

    public class ReportReferenceAccessor<TMap>: IReportAccessor
        where TMap: IReportMap
    {
        public ValueTask<object?> GetValue(object entity, ReportBuilder builder)
        {
            var map = builder.ServiceProvider.GetRequiredService<TMap>();
            return new ValueTask<object?>(map.Name(entity));
        }
    }
}
