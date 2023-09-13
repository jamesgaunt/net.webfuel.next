using System;
using System.Xml.Linq;

namespace Webfuel.Tools.Datafuel
{
    public class SchemaEntityPropertyEnum : SchemaEntityPropertyPrimative
    {
        public SchemaEntityPropertyEnum(SchemaEntity entity, XElement element, bool nullable, string tag)
            : base(entity, element, new PrimativeEnum(nullable, tag))
        {
        }

        public bool Flags
        {
            get { return ((PrimativeEnum)Primative).Flags; }
            set { ((PrimativeEnum)Primative).Flags = value; }
        }

        public override void GenerateValidation(ScriptBuilder sb)
        {
            var primative = (PrimativeEnum)Primative;

            if (!primative.Flags)
            {
                sb.Write("if(");
                if (Nullable)
                    sb.Write($"entity.{Name} != null && ");
                sb.WriteLine($"!Enum.IsDefined(typeof({primative.EnumType}), entity.{Name}{(Nullable ? ".Value" : "")})) throw new InvalidOperationException(\"{Name}: Not a defined value for enum {primative.EnumType}.\");");
            }
        }
    }
}
