using System;
using System.Xml.Linq;

namespace Webfuel.Tools.Datafuel
{
    public class SchemaEntityPropertyBoolean : SchemaEntityPropertyPrimative
    {
        public SchemaEntityPropertyBoolean(SchemaEntity entity, XElement element, bool nullable)
            : base(entity, element,  new PrimativeBoolean(nullable))
        {
        }
    }
}
