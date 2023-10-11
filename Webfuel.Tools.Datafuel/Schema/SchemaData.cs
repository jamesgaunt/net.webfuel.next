using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Webfuel.Tools.Datafuel
{
    public class SchemaData : SchemaElement
    {
        public SchemaData(SchemaEntity entity, XElement element)
            : base(entity.Schema, element)
        {
            Entity = entity;

            foreach (var row in element.Elements("Row"))
                Rows.Add(new SchemaDataRow(this, row));
        }

        public SchemaEntity Entity { get; private set; }

        public List<SchemaDataRow> Rows { get; } = new List<SchemaDataRow>();
    }

    public class SchemaDataRow : SchemaElement
    {
        public SchemaDataRow(SchemaData data, XElement element)
    :       base(data.Schema, element)
        {
            Data = data;
        }

        public SchemaData Data { get; private set; }

        public object GetValue(string name)
        {
            var member = Data.Entity.Members.FirstOrDefault(p => p.Name == name);
            if (member == null)
                throw new InvalidOperationException("The specified member does not exist");

            var attribute = GetAttribute(member, name);

            // Automatically set an unspecified sort order to the index of the row
            if (name == "SortOrder" && String.IsNullOrEmpty(attribute))
                return Data.Rows.IndexOf(this);

            return member.ParseValue(attribute)!;
        }

        string? GetAttribute(SchemaEntityMember member, string name)
        {
            if (Element.Attributes().Any(p => p.Name == name))
                return Element.Attribute(name)!.Value;

            return member.Default;
        }
    }
}
