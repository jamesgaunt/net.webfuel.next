using System;
using System.Xml.Linq;

namespace Webfuel.Tools.Datafuel
{
    public class SchemaEntityPropertyString : SchemaEntityPropertyPrimative
    {
        public SchemaEntityPropertyString(SchemaEntity entity, XElement element, bool nullable, string tag)
            : base(entity, element, new PrimativeString(nullable, (int?)ParseMinMaxTag(tag).max))
        {
            var minMax = ParseMinMaxTag(tag);
            Min = (int?)minMax.min;
            Max = (int?)minMax.max;
            Trim = element.BooleanProperty("Trim") ?? true;
            CaseSensitive = element.BooleanProperty("CaseSensitive") ?? false;
        }

        public int? Min { get; private set; }

        public int? Max { get; private set; }

        public bool Trim { get; private set; }

        // Generators

        public override void GenerateValidation(ScriptBuilder sb)
        {
            if (!Nullable)
                sb.WriteLine($"entity.{Name} = entity.{Name} ?? String.Empty;");

            if(Trim)
                sb.WriteLine($"entity.{Name} = entity.{Name}{(Nullable ? "?" : "")}.Trim();");

            if (Min.HasValue)
            {
                sb.Write("if(");
                if (Nullable)
                    sb.Write($"entity.{Name} != null && ");
                sb.WriteLine($"entity.{Name}.Length < {Min.Value}) throw new InvalidOperationException(\"{Name}: Cannot be shorter than {Min.Value} characters.\");");
            }

            if (Max.HasValue)
            {
                sb.Write("if(");
                if (Nullable)
                    sb.Write($"entity.{Name} != null && ");
                sb.WriteLine($"entity.{Name}.Length > {Max.Value}) throw new InvalidOperationException(\"{Name}: Cannot be longer than {Max.Value} characters.\");");
            }


        }
    }
}
