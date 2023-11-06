using System;

namespace Webfuel.Tools.Datafuel
{
    public class PrimativeDate: Primative
    {
        public PrimativeDate(bool nullable)
        {
            Nullable = nullable;
        }

        public int? Max { get; set; }

        public override string CLRType => "DateOnly";

        public override string SQLType => $"DATE";

        public override string CLRLiteral(object? value)
        {
            if (value == null)
                return "null";
            var d = (DateOnly)value;
            return $"new DateOnly({d.Year}, {d.Month}, {d.Day})";
        }

        public override string SQLLiteral(object? value)
        {
            if (value == null)
                return "NULL";
            var d = (DateOnly)value;
            return $"'{d.Year}-{d.Month}-{d.Day}'";
        }

        public override object? ParseValue(string? input)
        {
            if (input == null)
                return Nullable ? (DateOnly?)null : new DateOnly(1900, 1, 1);
            return DateOnly.Parse(input);
        }

        public override string ParameterMapper(SchemaEntityQueryParameter parameter)
        {
            if (Nullable)
                return $"new SqlParameter(\"{parameter.Name}\", (object?){parameter.Name.Replace("@", "").ToCamelCase()} ?? DBNull.Value)";
            return $"new SqlParameter(\"{parameter.Name}\", {parameter.Name.Replace("@", "").ToCamelCase()})";
        }
    }
}
