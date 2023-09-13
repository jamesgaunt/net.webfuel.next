using System;

namespace Webfuel.Tools.Datafuel
{
    public class PrimativeBoolean : Primative
    {
        public PrimativeBoolean(bool nullable)
        {
            Nullable = nullable;
        }

        public override string CLRType => "bool";

        public override string SQLType => "BIT";

        public override string CLRLiteral(object? value)
        {
            if (value == null)
                return "null";
            return Convert.ToBoolean(value) ? "true" : "false";
        }

        public override string SQLLiteral(object? value)
        {
            if (value == null)
                return "NULL";
            return Convert.ToBoolean(value) ? "1" : "0";
        }

        public override object? ParseValue(string? input) => input == null && Nullable ? null : (object)Boolean.Parse(input ?? "FALSE");
    }
}
