using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Webfuel.Tools.Datafuel
{
    public class SchemaEntityReference: SchemaEntityMember
    {
        public SchemaEntityReference(SchemaEntity entity, XElement element)
            : base(entity, element)
        {
            _referenceEntityName = element.StringProperty("Type") ?? throw new InvalidOperationException();

            if (!Name.EndsWith("Id"))
                throw new InvalidOperationException("Reference names must end with Id");

            CascadeDelete = element.BooleanProperty("CascadeDelete") ?? false;
        }

        public SchemaEntity ReferenceEntity => Schema.FindEntity(_referenceEntityName);
        string _referenceEntityName;

        public bool CascadeDelete { get; private set; }

        public override bool Nullable => _referenceEntityName.EndsWith("?");

        // Type

        public override string CLRType => ReferenceEntity.Key!.CLRType;

        public override string SQLType => ReferenceEntity.Key!.SQLType;

        public override object? ParseValue(string? input)
        {
            if (Nullable && String.IsNullOrEmpty(input))
                return null;
            return ReferenceEntity.Key!.ParseValue(input);
        }

        // Generators

        public override void GenerateEntityProperty(ScriptBuilder sb)
        {
            if (JsonIgnore)
                sb.WriteLine("[JsonIgnore]");
            sb.WriteLine($"{Access} {CLRTypeWithNullable} {Name} {{ get; {(InternalSet ? "internal " : "")}set; }}");
        }

        public override string GenerateSqlColumnDefinition()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[" + Name + "] " + SQLType);
            sb.Append(Nullable ? "" : " NOT NULL");

            if (!String.IsNullOrEmpty(Default))
            {
                if (!Guid.TryParse(Default, out var @default))
                    throw new InvalidOperationException($"Unable to parse reference default value '{Default}'");
                sb.Append(" CONSTRAINT [DF_" + Entity.Name + "_" + Name + "] DEFAULT('" + @default.ToString() + "')");
            }

            return sb.ToString();
        }
    }
}
