using System;
using System.Xml.Linq;

namespace Webfuel.Tools.Datafuel
{
    public class SchemaEntityPropertyInt32 : SchemaEntityPropertyPrimative
    {
        public SchemaEntityPropertyInt32(SchemaEntity entity, XElement element, bool nullable, string tag)
            : base(entity, element, new PrimativeInt32(nullable))
        {
            var minMax = ParseMinMaxTag(tag);
            Min = (int?)minMax.min;
            Max = (int?)minMax.max;
        }

        public int? Min { get; private set; }

        public int? Max { get; private set; }

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
