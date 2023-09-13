using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Webfuel
{
    [TypefuelInterface]
    public class Query
    {
        public List<string> Projection { get; set; } = new List<string>();

        public string? Search { get; set; }

        public string? Where { get; set; }

        public List<QueryFilter> Filters { get; set; } = new List<QueryFilter>();

        public List<QuerySort> Sort { get; set; } = new List<QuerySort>();

        public int Skip { get; set; }

        public int Take { get; set; }

        // Filter Builders

        public Query Add(Action<QueryFilterBuilder> action)
        {
            var filterBuilder = new QueryFilterBuilder(Filters);
            action(filterBuilder);
            return this;
        }

        public Query Any(Action<QueryFilterBuilder> action)
        {
            var filter = new QueryFilter { Op = QueryOp.Or, Filters = new List<QueryFilter>() };
            var filterBuilder = new QueryFilterBuilder(filter.Filters);
            action(filterBuilder);
            Filters.Add(filter);
            return this;
        }

        public Query All(Action<QueryFilterBuilder> action)
        {
            var filter = new QueryFilter { Op = QueryOp.And, Filters = new List<QueryFilter>() };
            var filterBuilder = new QueryFilterBuilder(filter.Filters);
            action(filterBuilder);
            Filters.Add(filter);
            return this;
        }

        // Filter Transform

        public void Transform(Func<QueryFilter, QueryFilter?> action)
        {
            Filters = Transform(Filters, action);
        }

        List<QueryFilter> Transform(List<QueryFilter> filters, Func<QueryFilter, QueryFilter?> action)
        {
            var result = new List<QueryFilter>();
            foreach(var filter in filters)
            {
                if(filter.Filters == null)
                {
                    // Clause
                    var transform = action(filter);
                    if (transform != null)
                        result.Add(transform);
                }
                else
                {
                    // And / Or
                    var transform = Transform(filter.Filters, action);
                    if (transform.Count > 0)
                        result.Add(new QueryFilter { Op = filter.Op, Filters = transform });
                }
            }
            return result;
        }
    }
}
