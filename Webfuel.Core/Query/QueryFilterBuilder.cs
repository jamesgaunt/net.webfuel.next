using System;
using System.Collections.Generic;

namespace Webfuel
{
    public class QueryFilterBuilder
    {
        public QueryFilterBuilder(List<QueryFilter> filters)
        {
            Filters = filters;
        }

        public List<QueryFilter> Filters { get; }

        public QueryFilterBuilder Equal(string field, object? value, bool condition)
        {
            if (!condition)
                return this;
            Filters.Add(new QueryFilter { Field = field, Op = QueryOp.Equal, Value = value });
            return this;
        }

        public QueryFilterBuilder Equal(string field, Guid value)
        {
            Filters.Add(new QueryFilter { Field = field, Op = QueryOp.Equal, Value = value });
            return this;
        }

        public QueryFilterBuilder Equal(string field, int value)
        {
            Filters.Add(new QueryFilter { Field = field, Op = QueryOp.Equal, Value = value });
            return this;
        }

        public QueryFilterBuilder NotEqual(string field, object? value, bool condition)
        {
            if (!condition)
                return this;
            Filters.Add(new QueryFilter { Field = field, Op = QueryOp.NotEqual, Value = value });
            return this;
        }

        public QueryFilterBuilder GreaterThan(string field, object value, bool condition)
        {
            if (!condition)
                return this;
            Filters.Add(new QueryFilter { Field = field, Op = QueryOp.GreaterThan, Value = value });
            return this;
        }

        public QueryFilterBuilder GreaterThanOrEqual(string field, object value, bool condition)
        {
            if (!condition)
                return this;
            Filters.Add(new QueryFilter { Field = field, Op = QueryOp.GreaterThanOrEqual, Value = value });
            return this;
        }

        public QueryFilterBuilder LessThan(string field, object value, bool condition)
        {
            if (!condition)
                return this;
            Filters.Add(new QueryFilter { Field = field, Op = QueryOp.LessThan, Value = value });
            return this;
        }

        public QueryFilterBuilder LessThanOrEqual(string field, object value, bool condition)
        {
            if (!condition)
                return this;
            Filters.Add(new QueryFilter { Field = field, Op = QueryOp.LessThanOrEqual, Value = value });
            return this;
        }

        public QueryFilterBuilder Contains(string field, string? value, bool condition = true)
        {
            if (!condition || String.IsNullOrEmpty(value))
                return this;
            Filters.Add(new QueryFilter { Field = field, Op = QueryOp.Contains, Value = value });
            return this;
        }
        public QueryFilterBuilder StartsWith(string field, string? value, bool condition = true)
        {
            if (!condition || String.IsNullOrEmpty(value))
                return this;
            Filters.Add(new QueryFilter { Field = field, Op = QueryOp.StartsWith, Value = value });
            return this;
        }
        public QueryFilterBuilder EndsWith(string field, string? value, bool condition = true)
        {
            if (!condition || String.IsNullOrEmpty(value))
                return this;
            Filters.Add(new QueryFilter { Field = field, Op = QueryOp.EndsWith, Value = value });
            return this;
        }

        public QueryFilterBuilder Any(Action<QueryFilterBuilder> action)
        {
            var filter = new QueryFilter { Op = QueryOp.Or, Filters = new List<QueryFilter>() };
            var filterBuilder = new QueryFilterBuilder(filter.Filters);
            action(filterBuilder);
            if (filter.Filters.Count > 0)
                Filters.Add(filter);
            return this;
        }

        public QueryFilterBuilder All(Action<QueryFilterBuilder> action)
        {
            var filter = new QueryFilter { Op = QueryOp.And, Filters = new List<QueryFilter>() };
            var filterBuilder = new QueryFilterBuilder(filter.Filters);
            action(filterBuilder);
            if (filter.Filters.Count > 0)
                Filters.Add(filter);
            return this;
        }
    }
}
