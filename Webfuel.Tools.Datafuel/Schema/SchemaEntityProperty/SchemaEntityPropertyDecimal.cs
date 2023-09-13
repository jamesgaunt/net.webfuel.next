using System;
using System.Xml.Linq;

namespace Webfuel.Tools.Datafuel
{
    public class SchemaEntityPropertyDecimal : SchemaEntityPropertyPrimative
    {
        public SchemaEntityPropertyDecimal(SchemaEntity entity, XElement element, bool nullable)
            : base(entity, element, new PrimativeDecimal(nullable))
        {
        }
    }
}
