using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Webfuel.Tools.Datafuel
{
    public static class StaticGenerator
    {
        public static void GenerateStatic(SchemaEntity entity)
        {
            File.WriteAllText(entity.GeneratedDirectory + $@"\{entity.Name}\{entity.Name}Static.cs", Static(entity));
        }

        static string Static(SchemaEntity entity)
        {
            var sb = new ScriptBuilder();

            Usings(sb, entity);

            using (sb.OpenBrace($"namespace {entity.Namespace}"))
            {
                sb.WriteLine("[ApiStatic]");
                using (sb.OpenBrace($"public static class {entity.Name}Static"))
                {
                    if (entity.Data != null)
                    {
                        foreach (var row in entity.Data.Rows)
                        {
                            Row(sb, row);
                        }
                    }
                    Values(sb, entity);
                    Map(sb, entity);
                }
            }

            return sb.ToString();
        }

        static void Row(ScriptBuilder sb, SchemaDataRow row)
        {
            if (row.Data!.Entity.Key!.CLRType == "Guid")
                sb.WriteLine($"public static readonly Guid {Identifier(row)} = Guid.Parse(\"{row.GetValue("Id")}\");");
            else
                sb.WriteLine($"public const int {Identifier(row)} = {row.GetValue("Id")};");
        }

        static string Identifier(SchemaDataRow row)
        {
            if (row.Data.Entity.Members.Any(p => p.Name == "UniqueId"))
                return row.GetValue("UniqueId")!.ToString()!.ToIdentifier();

            if (row.Data.Entity.Members.Any(p => p.Name == "Code"))
                return row.GetValue("Code")!.ToString()!.ToIdentifier();

            if (row.Data.Entity.Members.Any(p => p.Name == "Name"))
                return row.GetValue("Name")!.ToString()!.ToIdentifier();

            return "UNKNOWN";
        }

        static void Values(ScriptBuilder sb, SchemaEntity entity)
        {
            sb.WriteLine();
            sb.WriteLine($"public static readonly {entity.Name}[] Values = new {entity.Name}[] {{");
            {
                foreach (var row in entity.Data!.Rows)
                {
                    sb.Write($"new {entity.Name} {{ ");

                    var first = true;
                    foreach (var attribute in row.Element.Attributes())
                    {
                        var key = attribute.Name.ToString();
                        var value = attribute.Value;

                        var property = entity.FindMember(key) as SchemaEntityPropertyPrimative;
                        if (property == null)
                            continue;

                        if (!first)
                            sb.Write(", ");

                        sb.Write($"{key} = {property.CLRLiteral(property.ParseValue(value))}");
                        first = false;
                    }

                    sb.Write(" },\n");
                }
            }
            sb.WriteLine("};");
        }

        static void Map(ScriptBuilder sb, SchemaEntity entity)
        {
            sb.WriteLine();

            using(sb.OpenBrace($"public static {entity.Name} Map({(entity.Key!.CLRType == "Guid" ? "Guid" : "int")} id)"))
            {
                sb.WriteLine("return Values.Single(p => p.Id == id);");
            }
        }

        static void Usings(ScriptBuilder sb, SchemaEntity entity)
        {
            sb.WriteLine("using System;");
            sb.WriteLine("using System.Linq;");
        }
    }
}
