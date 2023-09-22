using System;

namespace Webfuel.Tools.Datafuel
{
    public class PrimativeDateTime: Primative
    {
        public PrimativeDateTime(bool nullable)
        {
            Nullable = nullable;
        }

        public int? Max { get; set; }

        public override string CLRType => "DateTimeOffset";

        public override string SQLType => $"DATETIMEOFFSET";

        public override string CLRLiteral(object? value)
        {
            if (value == null)
                return "null";
            var d = (DateTimeOffset)value;
            return $"new DateTimeOffset({d.Ticks}L, TimeSpan.Zero)";
        }

        public override string SQLLiteral(object? value)
        {
            if (value == null)
                return "NULL";
            var d = (DateTimeOffset)value;
            return $"'{d.Year}-{d.Month}-{d.Day} {d.Hour}:{d.Minute}:{d.Second}'";
        }

        public override object? ParseValue(string? input)
        {
            if (input == null)
                return Nullable ? (DateTimeOffset?)null : new DateTimeOffset(1900, 1, 1, 0, 0, 0, TimeSpan.Zero);
            return DateTimeOffset.Parse(input);
        }

        public override string ParameterMapper(SchemaEntityQueryParameter parameter)
        {
            if (Nullable)
                return $"new SqlParameter(\"{parameter.Name}\", (object?){parameter.Name.Replace("@", "").ToCamelCase()} ?? DBNull.Value)";
            return $"new SqlParameter(\"{parameter.Name}\", {parameter.Name.Replace("@", "").ToCamelCase()})";
        }
    }
}
