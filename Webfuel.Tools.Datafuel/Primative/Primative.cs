using System;
using System.Collections.Generic;
using System.Text;

namespace Webfuel.Tools.Datafuel
{
    public abstract class Primative
    {
        public bool Nullable { get; set; }

        public abstract string CLRType { get; }

        public virtual string CLRTypeWithNullable => CLRType + (Nullable ? "?" : "");

        public abstract string SQLType { get; }

        public abstract string CLRLiteral(object? value);

        public abstract string SQLLiteral(object? value);

        public abstract object? ParseValue(string? input);

        public virtual string ParameterMapper(SchemaEntityQueryParameter parameter) 
        {
            if (Nullable)
                return $"new SqlParameter(\"{parameter.Name}\", (object?){parameter.Name.Replace("@", "").ToCamelCase()} ?? DBNull.Value)";
            return $"new SqlParameter(\"{parameter.Name}\", {parameter.Name.Replace("@", "").ToCamelCase()})";
        }

        public static Primative Build(Schema schema, string? type)
        {
            var originalType = type;

            if (type == null)
                throw new InvalidOperationException();

            var tag = String.Empty;

            var nullable = false;

            if (type.EndsWith("?"))
            {
                nullable = true;
                type = type.Substring(0, type.Length - 1);
            }

            if (type.Contains("."))
            {
                var parts = type.Split('.');
                var member = schema.FindEntity(parts[0]).FindMember(parts[1]);

                if(member is SchemaEntityProperty)
                {
                    type = member.Element.StringProperty("Type") ?? String.Empty;
                    if (type.EndsWith("?"))
                    {
                        nullable = true;
                        type = type.Substring(0, type.Length - 1);
                    }
                }
                else if(member is SchemaEntityReference)
                {
                    var reference = member as SchemaEntityReference;
                    nullable = reference!.Nullable;
                    type = reference!.ReferenceEntity.Key!.Element.StringProperty("Type") ?? String.Empty;
                }
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
                    return new PrimativeBoolean(nullable);
                case "guid":
                    return new PrimativeGuid(nullable);
                case "int":
                case "int32":
                    return new PrimativeInt32(nullable);
                case "int64":
                    return new PrimativeInt64(nullable);
                case "string":
                    return new PrimativeString(nullable, max: null);
                case "double":
                    return new PrimativeDouble(nullable);
                case "decimal":
                    return new PrimativeDecimal(nullable);
                case "enum":
                    return new PrimativeEnum(nullable, tag);
                case "flags":
                    return new PrimativeEnum(nullable, tag) { Flags = true };
                case "meta:name":
                    return new PrimativeString(nullable, max: 64);
                case "meta:path":
                    return new PrimativeString(nullable, max: 256);
                case "meta:code":
                    return new PrimativeString(nullable, max: 32);
                case "meta:email":
                    return new PrimativeString(nullable, max: 64);
                case "meta:description":
                    return new PrimativeString(nullable, max: 1024);
                default:
                    throw new InvalidOperationException($"Invalid type code {type}");
            }
        }
    }
}
