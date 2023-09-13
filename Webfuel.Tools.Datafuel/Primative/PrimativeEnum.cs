using System;

namespace Webfuel.Tools.Datafuel
{
    public class PrimativeEnum: Primative
    {
        public PrimativeEnum(bool nullable, string enumType)
        {
            Nullable = nullable;
            EnumType = enumType;
        }

        public bool Flags { get; set; }

        public string EnumType { get; set; }

        public override string CLRType => EnumType;

        public override string SQLType => $"INT";

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
