using System;
using System.Xml.Linq;

namespace Webfuel.Tools.Datafuel
{
    public class SchemaEntityPropertyDouble : SchemaEntityPropertyPrimative
    {
        public SchemaEntityPropertyDouble(SchemaEntity entity, XElement element, bool nullable)
            : base(entity, element, new PrimativeDouble(nullable))
        {
        }
    }
}
