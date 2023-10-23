using System;
using System.Xml.Linq;

namespace Webfuel.Tools.Datafuel
{
    public class SchemaEntityPropertyJson : SchemaEntityProperty
    {
        public SchemaEntityPropertyJson(SchemaEntity entity, XElement element, bool nullable, string tag)
            : base(entity, element)
        {
            StructuredType = tag.Replace('{', '<').Replace('}', '>');
        }

        public string StructuredType { get; private set; }

        // Type

        public override string CLRType => StructuredType;

        public override string SQLType => "NVARCHAR(MAX)";

        public override bool Nullable => false;

        public override string CLRLiteral(object? value)
        {
            return "String.Empty";
        }

        public override string SQLLiteral(object? value)
        {
            return "N''";
        }

        public override object? ParseValue(string? input)
        {
            return input;
        }

        // Generators

        public override void GenerateEntityProperty(ScriptBuilder sb)
        {
            using (sb.OpenBrace($"public {CLRType} {Name}"))
            {
                sb.WriteLine($"get {{ return _{Name} ?? (_{Name} = SafeJsonSerializer.Deserialize<{CLRType}>(_{Name}Json)); }}");
                sb.WriteLine($"{(InternalSet ? "internal " : "")}set {{ _{Name} = value; }}");
            }
            sb.WriteLine($"{CLRType}? _{Name} = null;");

            using (sb.OpenBrace($"internal string {Name}Json"))
            {
                sb.WriteLine($"get {{ var result = _{Name} == null ? _{Name}Json : (_{Name}Json = SafeJsonSerializer.Serialize(_{Name})); _{Name} = null; return result; }}");
                sb.WriteLine($"set {{ _{Name}Json = value; _{Name} = null; }}");
            }
            sb.WriteLine($"string _{Name}Json = String.Empty;");
        }

        public override string GenerateRepositoryGetter(string prefix = "entity.")
        {
            return $"{prefix}{Name}Json";
        }

        public override string GenerateRepositorySetter(string prefix = "entity.")
        {
            return $"{prefix}{Name}Json = (string)value{(Nullable ? String.Empty : "!")};";
        }

        public override string GenerateComparisonGetter(string entity)
        {
            return $"{entity}.{Name}Json";
        }

        public override void GenerateCopy(ScriptBuilder sb)
        {
            sb.WriteLine($"entity.{Name}Json = {Name}Json;");
        }
    }
}
