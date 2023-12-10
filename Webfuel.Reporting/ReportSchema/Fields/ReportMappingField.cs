using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace Webfuel.Reporting
{
    public interface IReportMapping
    {
           Task<object?> Map(object context, IServiceProvider services);
    }

    public class ReportMapping<TEntity>: IReportMapping where TEntity : class
    {
        [JsonIgnore]
        public required Func<object, Guid?> Accessor { get; init; }

        [JsonIgnore]
        public IReportMapping? Mapping { get; init; }

        public async Task<object?> Map(object context, IServiceProvider services)
        {
            var id = Accessor(context);
            if (id == null)
                return null;

            var mapper = services.GetRequiredService<IReportMapper<TEntity>>();

            var entity = await mapper.Get(id.Value);
            if(entity == null)
                return null;

            return entity;
        }
    }
}
