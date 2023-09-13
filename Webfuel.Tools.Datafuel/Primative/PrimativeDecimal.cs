using System;

namespace Webfuel.Tools.Datafuel
{
    public class PrimativeDecimal: Primative
    {
        public PrimativeDecimal(bool nullable)
        {
            Nullable = nullable;
        }

        public override string CLRType => "Decimal";

        public override string SQLType => "DECIMAL(16, 2)";

        public override string CLRLiteral(object? value)
        {
            if (value == null)
                return "null";
            return Convert.ToDouble(value) + "M";
        }

        public override string SQLLiteral(object? value)
        {
            if (value == null)
                return "NULL";
            return Convert.ToDouble(value).ToString();
        }

        public override object? ParseValue(string? input) => input == null && Nullable ? null : (object)Decimal.Parse(input ?? "0");
    }
}
