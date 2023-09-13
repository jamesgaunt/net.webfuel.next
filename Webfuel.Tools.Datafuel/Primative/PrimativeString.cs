using System;

namespace Webfuel.Tools.Datafuel
{
    public class PrimativeString: Primative
    {
        public PrimativeString(bool nullable, int? max)
        {
            Nullable = nullable;
            Max = max;
        }

        public int? Max { get; set; }

        public override string CLRType => "string";

        public override string SQLType => $"NVARCHAR({(Max <= 4000 ? Max.ToString() : "MAX")})";

        public override string CLRLiteral(object? value)
        {
            if (value == null)
                return "null";
            if (value.ToString() == String.Empty)
                return "String.Empty";
            return "\"" + value.ToString() + "\"";
        }

        public override string SQLLiteral(object? value)
        {
            if (value == null)
                return "NULL";
            return "N'" + value.ToString() + "'";
        }

        public override object? ParseValue(string? input) => input == null && Nullable ? null : (object)(input ?? String.Empty);
    }
}
