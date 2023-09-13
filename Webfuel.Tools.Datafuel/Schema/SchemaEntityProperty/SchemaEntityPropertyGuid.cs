using System;
using System.Xml.Linq;

namespace Webfuel.Tools.Datafuel
{
    public class SchemaEntityPropertyGuid : SchemaEntityPropertyPrimative
    {
        public SchemaEntityPropertyGuid(SchemaEntity entity, XElement element, bool nullable)
            : base(entity, element, new PrimativeGuid(nullable))
        {
        }
    }
}
