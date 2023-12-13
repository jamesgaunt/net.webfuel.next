using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace Webfuel.Reporting
{
    internal class ReportMapping<TEntity> : IReportMapping 
        where TEntity : class 
    {
        public bool MultiValued => false;

        public required string Name { get; init; }

        [JsonIgnore]
        public IReportMapping? ParentMapping { get; init; }

        [JsonIgnore]
        public required Func<object, Guid?> Accessor { get; init; }

        public IReportMapper GetMapper(IServiceProvider services)
        {
            return (IReportMapper)services.GetRequiredService(typeof(IReportMapper<>).MakeGenericType(typeof(TEntity)));
        }

        public async Task<object?> Map(object context, ReportBuilder builder)
        {
            if (ParentMapping != null)
            {
                var parent = await ParentMapping.Map(context, builder);
                if (parent == null)
                    return null;
                context = parent;
            }

            var id = Accessor(context);
            if (id == null)
                return null;

            var mapper = GetMapper(builder.ServiceProvider);
            return await mapper.Get(id.Value);
        }
    }
}
