namespace Webfuel
{
    public class RepositoryQueryFilterBuilder
    {
        public RepositoryQueryFilterBuilder(List<RepositoryQueryFilter> filters)
        {
            Filters = filters;
        }

        public List<RepositoryQueryFilter> Filters { get; }

        public RepositoryQueryFilterBuilder Equal(string field, object? value, bool condition)
        {
            if (!condition)
                return this;
            Filters.Add(new RepositoryQueryFilter { Field = field, Op = RepositoryQueryOp.Equal, Value = value });
            return this;
        }

        public RepositoryQueryFilterBuilder Equal(string field, Guid value)
        {
            Filters.Add(new RepositoryQueryFilter { Field = field, Op = RepositoryQueryOp.Equal, Value = value });
            return this;
        }

        public RepositoryQueryFilterBuilder Equal(string field, int value)
        {
            Filters.Add(new RepositoryQueryFilter { Field = field, Op = RepositoryQueryOp.Equal, Value = value });
            return this;
        }

        public RepositoryQueryFilterBuilder NotEqual(string field, object? value, bool condition)
        {
            if (!condition)
                return this;
            Filters.Add(new RepositoryQueryFilter { Field = field, Op = RepositoryQueryOp.NotEqual, Value = value });
            return this;
        }

        public RepositoryQueryFilterBuilder GreaterThan(string field, object value, bool condition)
        {
            if (!condition)
                return this;
            Filters.Add(new RepositoryQueryFilter { Field = field, Op = RepositoryQueryOp.GreaterThan, Value = value });
            return this;
        }

        public RepositoryQueryFilterBuilder GreaterThanOrEqual(string field, object value, bool condition)
        {
            if (!condition)
                return this;
            Filters.Add(new RepositoryQueryFilter { Field = field, Op = RepositoryQueryOp.GreaterThanOrEqual, Value = value });
            return this;
        }

        public RepositoryQueryFilterBuilder LessThan(string field, object value, bool condition)
        {
            if (!condition)
                return this;
            Filters.Add(new RepositoryQueryFilter { Field = field, Op = RepositoryQueryOp.LessThan, Value = value });
            return this;
        }

        public RepositoryQueryFilterBuilder LessThanOrEqual(string field, object value, bool condition)
        {
            if (!condition)
                return this;
            Filters.Add(new RepositoryQueryFilter { Field = field, Op = RepositoryQueryOp.LessThanOrEqual, Value = value });
            return this;
        }

        public RepositoryQueryFilterBuilder Contains(string field, string? value, bool condition = true)
        {
            if (!condition || String.IsNullOrEmpty(value))
                return this;
            Filters.Add(new RepositoryQueryFilter { Field = field, Op = RepositoryQueryOp.Contains, Value = value });
            return this;
        }
        public RepositoryQueryFilterBuilder StartsWith(string field, string? value, bool condition = true)
        {
            if (!condition || String.IsNullOrEmpty(value))
                return this;
            Filters.Add(new RepositoryQueryFilter { Field = field, Op = RepositoryQueryOp.StartsWith, Value = value });
            return this;
        }
        public RepositoryQueryFilterBuilder EndsWith(string field, string? value, bool condition = true)
        {
            if (!condition || String.IsNullOrEmpty(value))
                return this;
            Filters.Add(new RepositoryQueryFilter { Field = field, Op = RepositoryQueryOp.EndsWith, Value = value });
            return this;
        }

        public RepositoryQueryFilterBuilder Any(Action<RepositoryQueryFilterBuilder> action)
        {
            var filter = new RepositoryQueryFilter { Op = RepositoryQueryOp.Or, Filters = new List<RepositoryQueryFilter>() };
            var filterBuilder = new RepositoryQueryFilterBuilder(filter.Filters);
            action(filterBuilder);
            if (filter.Filters.Count > 0)
                Filters.Add(filter);
            return this;
        }

        public RepositoryQueryFilterBuilder All(Action<RepositoryQueryFilterBuilder> action)
        {
            var filter = new RepositoryQueryFilter { Op = RepositoryQueryOp.And, Filters = new List<RepositoryQueryFilter>() };
            var filterBuilder = new RepositoryQueryFilterBuilder(filter.Filters);
            action(filterBuilder);
            if (filter.Filters.Count > 0)
                Filters.Add(filter);
            return this;
        }
    }
}
