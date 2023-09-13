using System;

namespace Webfuel.Tools.Datafuel
{
    public class PrimativeInt64 : Primative
    {
        public PrimativeInt64(bool nullable)
        {
            Nullable = nullable;
        }

        public override string CLRType => "Int64";

        public override string SQLType => "BIGINT";

        public override string CLRLiteral(object? value)
        {
            if (value == null)
                return "null";
            return Convert.ToInt64(value) + "L";
        }

        public override string SQLLiteral(object? value)
        {
            if (value == null)
                return "NULL";
            return Convert.ToInt32(value).ToString();
        }

        public override object? ParseValue(string? input) => input == null && Nullable ? null : (object)Int64.Parse(input ?? "0");
    }
}
