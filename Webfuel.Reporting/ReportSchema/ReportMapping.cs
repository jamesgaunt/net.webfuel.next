using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace Webfuel.Reporting
{
    internal interface IReportMapping
    {
        Task<ReportReference?> Map(object context, ReportBuilder builder);
    }

    internal class ReportMapping<TEntity> : IReportMapping where TEntity : class
    {
        [JsonIgnore]
        public required Func<object, Guid?> Accessor { get; init; }

        [JsonIgnore]
        public IReportMapping? Mapping { get; init; }

        public required string Name { get; init; }

        public async Task<ReportReference?> Map(object context, ReportBuilder builder)
        {
            if (Mapping != null)
            {
                var temp = await Mapping.Map(context, builder);
                if (temp == null)
                    return null;
                context = temp.Entity;
            }

            var id = Accessor(context);
            if (id == null)
                return null;

            var mapper = builder.ServiceProvider.GetRequiredService<IReportReferenceProvider<TEntity>>();

            var reference = await mapper.Get(id.Value);
            if (reference == null)
                return null;

            return reference;
        }
    }
}
