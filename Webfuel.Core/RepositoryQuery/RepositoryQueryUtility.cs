using Microsoft.Data.SqlClient;

namespace Webfuel
{
    internal static class RepositoryQueryUtility
    {
        public static string SelectSql(Query query, IEnumerable<string> fields)
        {
            if (query.Projection.Count == 0)
                return "SELECT " + String.Join(", ", fields.Select(p => Field(p)));
            return "SELECT " + String.Join(", ", query.Projection.Select(p => Field(p)));
        }

        public static string CountSql(Query query, IEnumerable<string> fields)
        {
            return $"SELECT COUNT({Field(fields.First())})";
        }

        public static string OrderSql(Query query, IEnumerable<string> fields, string defaultOrderBy)
        {
            if (query.Sort.Count == 0)
            {
                if (!String.IsNullOrEmpty(defaultOrderBy))
                    return defaultOrderBy;
                return $"ORDER BY {Field(fields.First())} ASC";
            }
            return "ORDER BY " + String.Join(", ", query.Sort.Select(p => $"{Field(p.Field)} {(p.Direction > 0 ? "ASC" : "DESC")}"));
        }

        public static string PageSql(Query query)
        {
            if (query.Skip == 0 && query.Take == 0)
                return String.Empty;
            return $"OFFSET {query.Skip} ROWS FETCH NEXT {(query.Take > 0 ? query.Take : 1000)} ROWS ONLY";
        }

        public static string Field(string field)
        {
            return "[" + field + "]";
        }

        public static List<SqlParameter>? SqlParameters(List<RepositoryQueryParameter> parameters)
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

        public static string FilterSql(Query query, List<RepositoryQueryParameter> parameters)
        {
            if (query.Filters.Count == 0)
                return String.Empty;

            return "WHERE " + String.Join(" AND ", query.Filters.Select(p => FilterSql(p, parameters)));
        }

        public static string FilterSql(QueryFilter filter, List<RepositoryQueryParameter> parameters)
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

                case QueryOp.SQL:
                    return filter.SQL;

            }
            throw new InvalidOperationException("Unrecognised Filter");
        }

        public static string PushParameter(object? value, List<RepositoryQueryParameter> parameters)
        {
            var name = "@P" + parameters.Count;
            parameters.Add(new RepositoryQueryParameter(name, value));
            return name;
        }

        public static void ValidateFields(Query query, IEnumerable<string> fields)
        {
            ValidateFields(query.Filters, fields);
            foreach (var field in query.Projection)
                ValidateField(field, fields);
            foreach (var sort in query.Sort)
                ValidateField(sort.Field, fields);
        }

        public static void ValidateFields(List<QueryFilter> filters, IEnumerable<string> fields)
        {
            foreach (var filter in filters)
            {
                if(filter.Op == QueryOp.And || filter.Op == QueryOp.Or)
                {
                    if (filter.Filters == null || filter.Filters.Count == 0)
                        throw new InvalidOperationException("Empty filter clause found in query");
                    ValidateFields(filter.Filters, fields);
                }
                else if(filter.Op == QueryOp.SQL)
                {
                    if (String.IsNullOrEmpty(filter.SQL))
                        throw new InvalidOperationException("Emply SQL clause found in query");
                }
                else
                {
                    ValidateField(filter.Field, fields);
                }
            }
        }

        public static void ValidateField(string field, IEnumerable<string> fields)
        {
            if (!fields.Contains(field, StringComparer.OrdinalIgnoreCase))
                throw new InvalidOperationException("Invalid field found in query");
        }
    }
}
