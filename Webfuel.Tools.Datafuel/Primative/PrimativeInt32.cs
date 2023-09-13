using System;

namespace Webfuel.Tools.Datafuel
{
    public class PrimativeInt32 : Primative
    {
        public PrimativeInt32(bool nullable)
        {
            Nullable = nullable;
        }

        public override string CLRType => "int";

        public override string SQLType => "INT";

        public override string CLRLiteral(object? value)
        {
            if (value == null)
                return "null";
            return Convert.ToInt32(value).ToString();
        }

        public override string SQLLiteral(object? value)
        {
            if (value == null)
                return "NULL";
            return Convert.ToInt32(value).ToString();
        }

        public override object? ParseValue(string? input) => input == null && Nullable ? null : (object)Int32.Parse(input ?? "0");
    }
}
