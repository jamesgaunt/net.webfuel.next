using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Webfuel.Tools.Datafuel
{
    public class SchemaElement
    {
        public SchemaElement(Schema schema, XElement element)
        {
            Schema = schema;
            Element = element;
        }

        public Schema Schema { get; private set; }

        public XElement Element { get; private set; }
    }
}
