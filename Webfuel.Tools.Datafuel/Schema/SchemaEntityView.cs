using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Webfuel.Tools.Datafuel
{
    public class SchemaEntityView: SchemaElement
    {
        public SchemaEntityView(SchemaEntity entity, XElement element)
            : base(entity.Schema, element)
        {
            Name = entity.Name;
            Sql = element.Element("Sql")!.Value;
        } 

        public string Name { get; private set; }

        public string Sql { get; private set; }
    }
}
