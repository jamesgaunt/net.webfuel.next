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

        /*
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
                sb.WriteLine($"entity.{Name} = value == DBNull.Value ? (Date?)null : ((DateTime)value!).ToDate();");
            else
                sb.WriteLine($"entity.{Name} = ((DateTime)value!).ToDate();");
        }
        */
    }
}
