using System;

namespace Webfuel.Tools.Datafuel
{
    public class PrimativeDateTimeUtc: Primative
    {
        public PrimativeDateTimeUtc(bool nullable)
        {
            Nullable = nullable;
        }

        public int? Max { get; set; }

        public override string CLRType => "DateTimeUtc";

        public override string SQLType => $"DATETIME";

        public override string CLRLiteral(object? value)
        {
            if (value == null)
                return "null";
            var d = (DateTimeUtc)value;
            return $"new DateTimeUtc({d.Ticks}L)";
        }

        public override string SQLLiteral(object? value)
        {
            if (value == null)
                return "NULL";
            var d = (DateTimeUtc)value;
            return $"'{d.Year}-{d.Month}-{d.Day} {d.Hour}:{d.Minute}:{d.Second}'";
        }

        public override object? ParseValue(string? input)
        {
            if (input == null)
                return Nullable ? (DateTimeUtc?)null : new DateTimeUtc(1900, 1, 1, 0, 0, 0);
            return DateTimeUtc.RobustParse(input);
        }

        public override string ParameterMapper(SchemaEntityQueryParameter parameter)
        {
            if (Nullable)
                return $"new SqlParameter(\"{parameter.Name}\", (object?){parameter.Name.Replace("@", "").ToCamelCase()}?.ToDateTime() ?? DBNull.Value)";
            return $"new SqlParameter(\"{parameter.Name}\", {parameter.Name.Replace("@", "").ToCamelCase()}.ToDateTime())";
        }
    }
}
