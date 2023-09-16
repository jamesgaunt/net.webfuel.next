using System;
using System.Xml.Linq;

namespace Webfuel.Tools.Datafuel
{
    public class SchemaEntityPropertyDateTimeUtc : SchemaEntityPropertyPrimative
    {
        public SchemaEntityPropertyDateTimeUtc(SchemaEntity entity, XElement element, bool nullable)
            : base(entity, element, new PrimativeDateTimeUtc(nullable))
        {
        }

        // Generators

        public override void GenerateRepositoryGetter(ScriptBuilder sb)
        {
            if (Nullable)
                sb.WriteLine($"return entity.{Name}?.ToDateTime();");
            else
                sb.WriteLine($"return entity.{Name}.ToDateTime();");
        }

        public override void GenerateRepositorySetter(ScriptBuilder sb)
        {
            if(Nullable)
                sb.WriteLine($"entity.{Name} = value == DBNull.Value ? (DateTimeUtc?)null : new DateTimeUtc((DateTime)value!);");
            else
                sb.WriteLine($"entity.{Name} = new DateTimeUtc((DateTime)value!);");
        }
    }
}
