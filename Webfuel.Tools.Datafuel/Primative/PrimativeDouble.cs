using System;

namespace Webfuel.Tools.Datafuel
{
    public class PrimativeDouble: Primative
    {
        public PrimativeDouble(bool nullable)
        {
            Nullable = nullable;
        }

        public override string CLRType => "Double";

        public override string SQLType => "FLOAT";

        public override string CLRLiteral(object? value)
        {
            if (value == null)
                return "null";
            return Convert.ToDouble(value) + "D";
        }

        public override string SQLLiteral(object? value)
        {
            if (value == null)
                return "NULL";
            return Convert.ToDouble(value).ToString();
        }

        public override object? ParseValue(string? input) => input == null && Nullable ? null : (object)Double.Parse(input ?? "0");
    }
}
