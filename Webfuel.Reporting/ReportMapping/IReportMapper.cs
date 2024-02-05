using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    public class ReferenceLookup
    {
        public required Guid Id { get; set; }

        public required string Name { get; set; }
    }


    /// <summary>
    /// Marker interface that marks this service as the IReportMapper for a specific entity type
    /// </summary>
    public interface IReportMapper<TEntity> : IReportMapper where TEntity : class
    {
    }

    public interface IReportMapper
    {
        Task<object?> Get(Guid id);

        Task<QueryResult<object>> Query(Query query);

        async Task<QueryResult<ReferenceLookup>> Lookup(Query query)
        {
            var result = await Query(query);
            return new QueryResult<ReferenceLookup>
            {
                TotalCount = result.TotalCount,
                Items = result.Items.Select(x => new ReferenceLookup { Id = Id(x), Name = DisplayName(x) }).ToList()
            };
        }

        Guid Id(object reference);

        string DisplayName(object reference);
    }
}
