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

        public override string GenerateRepositorySetter(string prefix = "entity.")
        {
            if(Nullable)
                return $"{prefix}{Name} = value == DBNull.Value ? (DateOnly?)null : DateOnly.FromDateTime((DateTime)value!);";

            return $"{prefix}{Name} = DateOnly.FromDateTime((DateTime)value!);";
        }
    }
}
