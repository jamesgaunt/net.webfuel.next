using System;
using System.Xml.Linq;

namespace Webfuel.Tools.Datafuel
{
    public class SchemaEntityPropertyDate : SchemaEntityPropertyPrimative
    {
        public SchemaEntityPropertyDate(SchemaEntity entity, XElement element, bool nullable)
            : base(entity, element, new PrimativeDate(nullable))
        {
        }

        // Generators

        public override void GenerateRepositorySetter(ScriptBuilder sb)
        {
            if(Nullable)
                sb.WriteLine($"entity.{Name} = value == DBNull.Value ? (DateOnly?)null : DateOnly.FromDateTime((DateTime)value!);");
            else
                sb.WriteLine($"entity.{Name} = DateOnly.FromDateTime((DateTime)value!);");
        }
    }
}
