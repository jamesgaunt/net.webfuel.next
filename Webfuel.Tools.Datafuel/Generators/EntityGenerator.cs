using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Webfuel.Tools.Datafuel
{
    static class EntityGenerator
    {
        public static void GenerateEntity(SchemaEntity entity)
        {
            Directory.CreateDirectory(entity.GeneratedDirectory + @"\" + entity.Name);

            File.WriteAllText(entity.GeneratedDirectory + $@"\{entity.Name}\{entity.Name}.cs", Entity(entity));

            if (entity.Repository)
                RepositoryGenerator.GenerateRepository(entity);

            if (entity.Static)
                StaticGenerator.GenerateStatic(entity);
        }

        static void Usings(ScriptBuilder sb, SchemaEntity entity)
        {
            sb.WriteLine("using FluentValidation;");
            sb.WriteLine("using Microsoft.Data.SqlClient;");
            sb.WriteLine("using System.Text.Json.Serialization;");
            sb.WriteLine("");
        }

        static string Entity(SchemaEntity entity)
        {
            var sb = new ScriptBuilder();

            Usings(sb, entity);
            using (sb.OpenBrace($"namespace {entity.Namespace}"))
            {
                using (sb.OpenBrace($"public partial class {entity.Name}{Interface(entity)}"))
                {
                    Constructor(sb, entity);

                    foreach (var member in entity.Members)
                        member.GenerateEntityProperty(sb);

                    Copy(sb, entity);
                }
            }

            return sb.ToString();
        }

        static void Constructor(ScriptBuilder sb, SchemaEntity entity)
        {
            sb.WriteLine($"public {entity.Name}() {{ }}");
            sb.WriteLine();

            using (sb.OpenBrace($"public {entity.Name}(SqlDataReader dr)"))
            {
                using (sb.OpenBrace($"for (var i = 0; i < dr.FieldCount; i++)"))
                {
                    sb.WriteLine("var value = dr.GetValue(i);");
                    sb.WriteLine("var property = dr.GetName(i);");
                    sb.WriteLine();

                    using (sb.OpenBrace($"switch (property)"))
                    {
                        foreach (var member in entity.Members)
                        {
                            sb.WriteLine($"case nameof({entity.Name}.{member.Name}):");
                            sb.WriteLine(member.GenerateRepositorySetter(String.Empty));
                            sb.WriteLine("break;");
                        }
                    }
                }
            }
        }

        static string Interface(SchemaEntity entity)
        {
            var result = String.Empty;

            if (!String.IsNullOrEmpty(entity.Interface))
            {
                if (result != String.Empty)
                    result += ", ";
                result += entity.Interface;
            }

            if (String.IsNullOrEmpty(result))
                return String.Empty;
            return ": " + result;
        }

        static void Copy(ScriptBuilder sb, SchemaEntity entity)
        {
            using (sb.OpenBrace($"public {entity.Name} Copy()"))
            {
                sb.WriteLine($"var entity = new {entity.Name}();");
                foreach (var member in entity.Members)
                    member.GenerateCopy(sb);
                sb.WriteLine("return entity;");
            }
        }
    }
}
