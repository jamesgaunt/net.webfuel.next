using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Webfuel.Tools.Datafuel
{
    public abstract class SchemaEntityProperty : SchemaEntityMember
    {
        public SchemaEntityProperty(SchemaEntity entity, XElement element)
            : base(entity, element)
        {
        }

        // Properties

        public bool CaseSensitive { get; protected set; }

        // Type

        public abstract string CLRLiteral(object? value);

        public abstract string SQLLiteral(object? value);

        // Generators

        public override void GenerateEntityProperty(ScriptBuilder sb)
        {
            if (JsonIgnore)
                sb.WriteLine("[JsonIgnore]");
            sb.Write($"{Access} {CLRTypeWithNullable} {Name}  {{ get; {(InternalSet?"internal " : "")}set; }}");
            sb.Write($" = {CLRLiteral(ParseValue(Default))};");
            sb.WriteLine();
        }

        public override string GenerateSqlColumnDefinition()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[" + Name + "] " + SQLType);
            if (CaseSensitive)
                sb.Append(" COLLATE SQL_Latin1_General_CP1_CS_AS");
            sb.Append(Nullable ? "" : " NOT NULL");
            sb.Append(" CONSTRAINT [DF_" + Entity.Name + "_" + Name + "] DEFAULT(" + SQLLiteral(ParseValue(Default)) + ")");
            return sb.ToString();
        }

        // Static Factory

        public static SchemaEntityProperty Build(SchemaEntity entity, XElement element)
        {
            var tag = String.Empty;
            var type = element.StringProperty("Type") ?? String.Empty;

            var nullable = false;

            if (type.EndsWith("?"))
            {
                nullable = true;
                type = type.Substring(0, type.Length - 1);
            }

            if (type.Contains("."))
            {
                var parts = type.Split('.');
                var property = entity.Schema.FindEntity(parts[0]).FindMember(parts[1]);
                type = property.Element.StringProperty("Type") ?? String.Empty;

                if (type.EndsWith("?"))
                {
                    nullable = true;
                    type = type.Substring(0, type.Length - 1);
                }
            }

            if(type.StartsWith("json:"))
            {
                tag = type.Substring("json:".Length);
                type = "json";
            }

            if (type.StartsWith("enum:"))
            {
                tag = type.Substring("enum:".Length);
                type = "enum";
            }

            if (type.StartsWith("flags:"))
            {
                tag = type.Substring("flags:".Length);
                type = "flags";
            }

            if (type.EndsWith("]"))
            {
                var s = type.IndexOf('[');
                tag = type.Substring(s + 1, type.Length - s - 2);
                type = type.Substring(0, s);
            }
            
            switch (type.ToLower())
            {
                case "bool":
                case "boolean":
                    return new SchemaEntityPropertyBoolean(entity, element, nullable);
                case "guid":
                    return new SchemaEntityPropertyGuid(entity, element, nullable);
                case "int":
                case "int32":
                    return new SchemaEntityPropertyInt32(entity, element, nullable, tag);
                case "int64":
                    return new SchemaEntityPropertyInt64(entity, element, nullable, tag);
                case "string":
                    return new SchemaEntityPropertyString(entity, element, nullable, tag);
                case "double":
                    return new SchemaEntityPropertyDouble(entity, element, nullable);
                case "decimal":
                    return new SchemaEntityPropertyDecimal(entity, element, nullable);
                case "json":
                    return new SchemaEntityPropertyJson(entity, element, nullable, tag);
                case "enum":
                    return new SchemaEntityPropertyEnum(entity, element, nullable, tag);
                case "flags":
                    return new SchemaEntityPropertyEnum(entity, element, nullable, tag) { Flags = true };
                case "date":
                    return new SchemaEntityPropertyDate(entity, element, nullable);
                case "datetime":
                    return new SchemaEntityPropertyDateTime(entity, element, nullable);

                case "meta:name":
                    return new SchemaEntityPropertyString(entity, element, nullable, "64");
                case "meta:path":
                    return new SchemaEntityPropertyString(entity, element, nullable, "256");
                case "meta:code":
                    return new SchemaEntityPropertyString(entity, element, nullable, "32");
                case "meta:email":
                    return new SchemaEntityPropertyString(entity, element, nullable, "64");
                case "meta:description":
                    return new SchemaEntityPropertyString(entity, element, nullable, "1024");

                default:
                    throw new InvalidOperationException($"Invalid type code {type}");
            }
        }

        // Helpers

        protected static (long? min, long? max) ParseMinMaxTag(string tag)
        {
            long? min = null;
            long? max = null;

            if (String.IsNullOrEmpty(tag))
                return (min, max);

            if(tag.Contains(','))
            {
                var parts = tag.Split(',');
                min = long.Parse(parts[0]);
                max = long.Parse(parts[1]);
            }
            else
            {
                max = long.Parse(tag);
            }

            return (min, max);
        }
    }
}
