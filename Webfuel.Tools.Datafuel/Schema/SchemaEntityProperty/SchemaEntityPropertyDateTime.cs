using System;
using System.Xml.Linq;

namespace Webfuel.Tools.Datafuel
{
    public class SchemaEntityPropertyDateTime : SchemaEntityPropertyPrimative
    {
        public SchemaEntityPropertyDateTime(SchemaEntity entity, XElement element, bool nullable)
            : base(entity, element, new PrimativeDateTime(nullable))
        {
        }
    }
}
