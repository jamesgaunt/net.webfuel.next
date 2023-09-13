using System;

namespace Webfuel.Tools.Datafuel
{
    public class PrimativeGuid: Primative
    {
        public PrimativeGuid(bool nullable)
        {
            Nullable = nullable;
        }

        public override string CLRType => "Guid";

        public override string SQLType => "UNIQUEIDENTIFIER";

        public override string CLRLiteral(object? value)
        {
            if (value == null)
                return "null";
            Guid g = value is Guid ? (Guid)value : Guid.Parse(value.ToString() ?? String.Empty);
            if (g == Guid.Empty)
                return "Guid.Empty";
            return "Guid.Parse(\"" + g + "\")";
        }

        public override string SQLLiteral(object? value)
        {
            if (value == null)
                return "NULL";
            Guid g = value is Guid ? (Guid)value : Guid.Parse(value.ToString() ?? String.Empty);
            return "'" + g + "'";
        }

        public override object? ParseValue(string? input) => input == null && Nullable ? null : (object)(input == null ? Guid.Empty : Guid.Parse(input));
    }
}
