using System.Text;

namespace Webfuel
{
    public class RepositoryQueryFilter
    {
        public RepositoryQueryFilter() { }

        public RepositoryQueryFilter(string field, string op, object? value)
        {
            Field = field;
            Op = op;
            Value = value;
        }

        public string Field { get; set; } = String.Empty;

        public string Op { get; set; } = RepositoryQueryOp.None;

        public object? Value { get; set; }

        public List<RepositoryQueryFilter>? Filters { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (Filters == null)
                sb.Append($"{Field} {Op} {FormatValue(Value)}");
            else
                sb.Append("(" + String.Join($" {Op} ", Filters) + ")");
            return sb.ToString();
        }

        string? FormatValue(object? value)
        {
            if (value == null)
                return "null";

            if (value is String)
                return $"\'{EscapeString(value.ToString()!)}\'";

            if (value is Boolean)
                return ((Boolean)value) ? "true" : "false";

            return value.ToString();
        }

        string EscapeString(string input)
        {
            if (String.IsNullOrEmpty(input))
                return input;

            return input.Replace("\\", "\\\\").Replace("\n", "\\n").Replace("\t", "\\t").Replace("\r", "\\r").Replace("\"", "\\").Replace("\'", "\\\'");
        }

    }
}
