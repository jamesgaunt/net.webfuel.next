using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Webfuel
{
    public static class RepositoryQueryUtility
    {
        public static string SelectSql(Query query, List<string> fields)
        {
            if (query.Projection.Count == 0 || query.Projection.All(p => !ValidField(fields, p)))
                return "SELECT " + String.Join(", ", fields.Select(p => Field(p)));
            return "SELECT " + String.Join(", ", query.Projection.Where(p => ValidField(fields, p)).Select(p => Field(p)));
        }

        public static string CountSql(Query query, List<string> fields)
        {
            return $"SELECT COUNT({Field(fields[0])})";
        }

        public static string OrderSql(Query query, List<string> fields, string defaultOrderBy)
        {
            if (query.Sort.Count == 0 || query.Sort.All(p => !ValidField(fields, p.Field)))
            {
                if (!String.IsNullOrEmpty(defaultOrderBy))
                    return defaultOrderBy;
                return $"ORDER BY {Field(fields[0])} ASC";
            }
            return "ORDER BY " + String.Join(", ", query.Sort.Where(p => ValidField(fields, p.Field)).Select(p => $"{Field(p.Field)} {(p.Direction > 0 ? "ASC" : "DESC")}"));
        }

        public static string PageSql(Query query)
        {
            if (query.Skip == 0 && query.Take == 0)
                return String.Empty;
            return $"OFFSET {query.Skip} ROWS FETCH NEXT {(query.Take > 0 ? query.Take : 99999999)} ROWS ONLY";
        }

        public static string Field(string field)
        {
            return "[" + field + "]";
        }

        public static bool ValidField(IEnumerable<string> fields, string field)
        {
            foreach (var f in fields)
            {
                if (String.Compare(f, field, true) == 0)
                    return true;
            }
            return false;
        }

        public static List<SqlParameter>? SqlParameters(List<QueryParameter> parameters)
        {
            if (parameters.Count == 0)
                return null;
            var sqlParameters = new List<SqlParameter>();
            foreach (var item in parameters)
            {
                sqlParameters.Add(new SqlParameter
                {
                    ParameterName = item.Name,
                    Value = item.Value
                });
            }
            return sqlParameters;
        }

        public static string FilterSql(Query query, List<QueryParameter> parameters)
        {
            if (query.Filters.Count == 0)
                return String.Empty;

            return "WHERE " + String.Join(" AND ", query.Filters.Select(p => FilterSql(p, parameters)));
        }

        public static string FilterSql(QueryFilter filter, List<QueryParameter> parameters)
        {
            switch (filter.Op)
            {
                case QueryOp.Equal:
                    if (filter.Value == null)
                        return $"{Field(filter.Field)} IS NULL";
                    return $"{Field(filter.Field)} = {PushParameter(filter.Value, parameters)}";

                case QueryOp.NotEqual:
                    if (filter.Value == null)
                        return $"{Field(filter.Field)} IS NOT NULL";
                    return $"{Field(filter.Field)} != {PushParameter(filter.Value, parameters)}";

                case QueryOp.Contains:
                    return $"{Field(filter.Field)} LIKE ('%' + {PushParameter(filter.Value, parameters)} + '%')";

                case QueryOp.StartsWith:
                    return $"{Field(filter.Field)} LIKE ({PushParameter(filter.Value, parameters)} + '%')";

                case QueryOp.EndsWith:
                    return $"{Field(filter.Field)} LIKE ('%' + {PushParameter(filter.Value, parameters)})";

                case QueryOp.And:
                    if (filter.Filters == null || filter.Filters.Count == 0)
                        return "(TRUE)";
                    return "(" + String.Join(" AND ", filter.Filters.Select(p => FilterSql(p, parameters))) + ")";

                case QueryOp.Or:
                    if (filter.Filters == null || filter.Filters.Count == 0)
                        return "(TRUE)";
                    return "(" + String.Join(" OR ", filter.Filters.Select(p => FilterSql(p, parameters))) + ")";

                case QueryOp.LessThan:
                    return $"{Field(filter.Field)} < {PushParameter(filter.Value, parameters)}";

                case QueryOp.GreaterThan:
                    return $"{Field(filter.Field)} > {PushParameter(filter.Value, parameters)}";

                case QueryOp.LessThanOrEqual:
                    return $"{Field(filter.Field)} <= {PushParameter(filter.Value, parameters)}";

                case QueryOp.GreaterThanOrEqual:
                    return $"{Field(filter.Field)} >= {PushParameter(filter.Value, parameters)}";

            }
            throw new InvalidOperationException("Unrecognised Filter");
        }

        public static string PushParameter(object? value, List<QueryParameter> parameters)
        {
            var name = "@P" + parameters.Count;
            parameters.Add(new QueryParameter(name, value));
            return name;
        }

        public static void PurgeFilters(Query query, IEnumerable<string> fields)
        {
            query.Transform((filter) =>
            {
                if (!ValidField(fields, filter.Field))
                    return null;
                return filter;
            });
        }
    }
}
