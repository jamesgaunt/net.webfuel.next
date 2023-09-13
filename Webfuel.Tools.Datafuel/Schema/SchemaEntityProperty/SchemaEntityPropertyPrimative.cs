using System;
using System.Xml.Linq;

namespace Webfuel.Tools.Datafuel
{
    public class SchemaEntityPropertyPrimative : SchemaEntityProperty
    {
        public SchemaEntityPropertyPrimative(SchemaEntity entity, XElement element, Primative primative)
            : base(entity, element)
        {
            Primative = primative;
        }

        protected Primative Primative;

        // Type

        public override bool Nullable => Primative.Nullable;

        public override string CLRType => Primative.CLRType;

        public override string SQLType => Primative.SQLType;

        public override string CLRLiteral(object? value) => Primative.CLRLiteral(value);

        public override string SQLLiteral(object? value) => Primative.SQLLiteral(value);

        public override object? ParseValue(string? input) => Primative.ParseValue(input);
    }
}
