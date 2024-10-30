using System.Security.Cryptography.Xml;

namespace Webfuel
{
    [ApiIgnore]
    public interface IQueryFilterBuilder
    {
        List<QueryFilter> Filters { get; }
    }

    public class QueryFilterBuilder: IQueryFilterBuilder
    {
        public QueryFilterBuilder(List<QueryFilter> filters)
        {
            Filters = filters;
        }

        public List<QueryFilter> Filters { get; }
    }

    public static class QueryFilterBuilderExtensions
    {
        public static IQueryFilterBuilder SQL(this IQueryFilterBuilder builder, string sql, bool condition = true)
        {
            if (!condition)
                return builder;

            builder.Filters.Add(new QueryFilter { Op = QueryOp.SQL, SQL = sql });
            return builder;
        }

        public static IQueryFilterBuilder Equal(this IQueryFilterBuilder builder, string field, object? value, bool condition)
        {
            if (!condition)
                return builder;
            builder.Filters.Add(new QueryFilter { Field = field, Op = QueryOp.Equal, Value = value });
            return builder;
        }

        public static IQueryFilterBuilder Equal(this IQueryFilterBuilder builder, string field, Guid value)
        {
            builder.Filters.Add(new QueryFilter { Field = field, Op = QueryOp.Equal, Value = value });
            return builder;
        }

        public static IQueryFilterBuilder Equal(this IQueryFilterBuilder builder, string field, int value)
        {
            builder.Filters.Add(new QueryFilter { Field = field, Op = QueryOp.Equal, Value = value });
            return builder;
        }

        public static IQueryFilterBuilder Equal(this IQueryFilterBuilder builder, string field, bool value)
        {
            builder.Filters.Add(new QueryFilter { Field = field, Op = QueryOp.Equal, Value = value });
            return builder;
        }

        public static IQueryFilterBuilder NotEqual(this IQueryFilterBuilder builder, string field, object? value, bool condition)
        {
            if (!condition)
                return builder;
            builder.Filters.Add(new QueryFilter { Field = field, Op = QueryOp.NotEqual, Value = value });
            return builder;
        }

        public static IQueryFilterBuilder GreaterThan(this IQueryFilterBuilder builder, string field, object? value, bool condition)
        {
            if (!condition)
                return builder;
            builder.Filters.Add(new QueryFilter { Field = field, Op = QueryOp.GreaterThan, Value = value });
            return builder;
        }

        public static IQueryFilterBuilder GreaterThanOrEqual(this IQueryFilterBuilder builder, string field, object? value, bool condition)
        {
            if (!condition)
                return builder;
            builder.Filters.Add(new QueryFilter { Field = field, Op = QueryOp.GreaterThanOrEqual, Value = value });
            return builder;
        }

        public static IQueryFilterBuilder LessThan(this IQueryFilterBuilder builder, string field, object? value, bool condition)
        {
            if (!condition)
                return builder;
            builder.Filters.Add(new QueryFilter { Field = field, Op = QueryOp.LessThan, Value = value });
            return builder;
        }

        public static IQueryFilterBuilder LessThanOrEqual(this IQueryFilterBuilder builder, string field, object? value, bool condition)
        {
            if (!condition)
                return builder;
            builder.Filters.Add(new QueryFilter { Field = field, Op = QueryOp.LessThanOrEqual, Value = value });
            return builder;
        }

        public static IQueryFilterBuilder Contains(this IQueryFilterBuilder builder, string field, string? value, bool condition = true)
        {
            if (!condition || String.IsNullOrEmpty(value))
                return builder;
            builder.Filters.Add(new QueryFilter { Field = field, Op = QueryOp.Contains, Value = value });
            return builder;
        }
        public static IQueryFilterBuilder StartsWith(this IQueryFilterBuilder builder, string field, string? value, bool condition = true)
        {
            if (!condition || String.IsNullOrEmpty(value))
                return builder;
            builder.Filters.Add(new QueryFilter { Field = field, Op = QueryOp.StartsWith, Value = value });
            return builder;
        }
        public static IQueryFilterBuilder EndsWith(this IQueryFilterBuilder builder, string field, string? value, bool condition = true)
        {
            if (!condition || String.IsNullOrEmpty(value))
                return builder;
            builder.Filters.Add(new QueryFilter { Field = field, Op = QueryOp.EndsWith, Value = value });
            return builder;
        }

        public static IQueryFilterBuilder Any(this IQueryFilterBuilder builder, Action<QueryFilterBuilder> action)
        {
            var filter = new QueryFilter { Op = QueryOp.Or, Filters = new List<QueryFilter>() };
            var filterBuilder = new QueryFilterBuilder(filter.Filters);
            action(filterBuilder);
            if (filter.Filters.Count > 0)
                builder.Filters.Add(filter);
            return builder;
        }

        public static IQueryFilterBuilder All(this IQueryFilterBuilder builder, Action<QueryFilterBuilder> action)
        {
            var filter = new QueryFilter { Op = QueryOp.And, Filters = new List<QueryFilter>() };
            var filterBuilder = new QueryFilterBuilder(filter.Filters);
            action(filterBuilder);
            if (filter.Filters.Count > 0)
                builder.Filters.Add(filter);
            return builder;
        }
    }
}
