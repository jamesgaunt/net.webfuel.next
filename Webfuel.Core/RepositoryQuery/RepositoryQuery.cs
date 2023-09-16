namespace Webfuel
{
    public class RepositoryQuery
    {
        public List<string> Projection { get; set; } = new List<string>();

        public List<RepositoryQueryFilter> Filters { get; set; } = new List<RepositoryQueryFilter>();

        public List<QuerySort> Sort { get; set; } = new List<QuerySort>();

        public int Skip { get; set; }

        public int Take { get; set; }

        // Filter Builders

        public RepositoryQuery Add(Action<RepositoryQueryFilterBuilder> action)
        {
            var filterBuilder = new RepositoryQueryFilterBuilder(Filters);
            action(filterBuilder);
            return this;
        }

        public RepositoryQuery Any(Action<RepositoryQueryFilterBuilder> action)
        {
            var filter = new RepositoryQueryFilter { Op = RepositoryQueryOp.Or, Filters = new List<RepositoryQueryFilter>() };
            var filterBuilder = new RepositoryQueryFilterBuilder(filter.Filters);
            action(filterBuilder);
            Filters.Add(filter);
            return this;
        }

        public RepositoryQuery All(Action<RepositoryQueryFilterBuilder> action)
        {
            var filter = new RepositoryQueryFilter { Op = RepositoryQueryOp.And, Filters = new List<RepositoryQueryFilter>() };
            var filterBuilder = new RepositoryQueryFilterBuilder(filter.Filters);
            action(filterBuilder);
            Filters.Add(filter);
            return this;
        }

        // Filter Transform

        public void Transform(Func<RepositoryQueryFilter, RepositoryQueryFilter?> action)
        {
            Filters = Transform(Filters, action);
        }

        List<RepositoryQueryFilter> Transform(List<RepositoryQueryFilter> filters, Func<RepositoryQueryFilter, RepositoryQueryFilter?> action)
        {
            var result = new List<RepositoryQueryFilter>();
            foreach (var filter in filters)
            {
                if (filter.Filters == null)
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
                        result.Add(new RepositoryQueryFilter { Op = filter.Op, Filters = transform });
                }
            }
            return result;
        }
    }
}
