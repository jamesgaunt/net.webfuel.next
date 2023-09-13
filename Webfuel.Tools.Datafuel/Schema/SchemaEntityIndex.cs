using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Webfuel.Tools.Datafuel
{
    public class SchemaEntityIndex: SchemaElement
    {
        public SchemaEntityIndex(SchemaEntity entity, XElement element)
            : base(entity.Schema, element)
        {
            Entity = entity;

            Unique = element.BooleanProperty("Unique") ?? false;

            foreach (var member in element.Elements("Member"))
                Members.Add(entity.FindMember(member.StringProperty("Name")));

            Repository = element.BooleanProperty("Repository") ?? (Entity.Key == null || !Members.Contains(Entity.Key));
        }

        public SchemaEntity Entity { get; private set; }

        public bool Unique { get; private set; }

        public bool Repository { get; private set; } = true;

        public List<SchemaEntityMember> Members { get; } = new List<SchemaEntityMember>();
    }
}
