using System;
using System.Xml.Linq;

namespace Webfuel.Tools.Datafuel
{
    public class SchemaEntityPropertyInt64 : SchemaEntityPropertyPrimative
    {
        public SchemaEntityPropertyInt64(SchemaEntity entity, XElement element, bool nullable, string tag)
            : base(entity, element, new PrimativeInt64(nullable))
        {
            var minMax = ParseMinMaxTag(tag);
            Min = minMax.min;
            Max = minMax.max;
        }

        public long? Min { get; private set; }

        public long? Max { get; private set; }

        // Generators

        public override void GenerateValidation(ScriptBuilder sb)
        {
            if (Min.HasValue)
            {
                sb.Write("if(");
                if (Nullable)
                    sb.Write($"entity.{Name} != null && ");
                sb.WriteLine($"entity.{Name} < {Min.Value}) throw new InvalidOperationException(\"{Name}: Cannot be less than {Min.Value}.\");");
            }

            if (Max.HasValue)
            {
                sb.Write("if(");
                if (Nullable)
                    sb.Write($"entity.{Name} != null && ");
                sb.WriteLine($"entity.{Name} > {Max.Value}) throw new InvalidOperationException(\"{Name}: Cannot be more than {Max.Value}.\");");
            }
        }
    }
}
