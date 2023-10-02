using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Tools.Datafuel
{
    static class MetadataGenerator
    {

        public static void GenerateMetadata(SchemaEntity entity)
        {
            Directory.CreateDirectory(entity.GeneratedDirectory + @"\" + entity.Name);

            File.WriteAllText(entity.GeneratedDirectory + $@"\{entity.Name}\{entity.Name}Metadata.cs", Metadata(entity));
        }

        static void Usings(ScriptBuilder sb, SchemaEntity entity)
        {
            sb.WriteLine("using FluentValidation;");
            sb.WriteLine("using Microsoft.Data.SqlClient;");

            sb.WriteLine("using System.Text.Json.Serialization;");
            sb.WriteLine("");
        }

        static string Metadata(SchemaEntity entity)
        {
            var sb = new ScriptBuilder();

            Usings(sb, entity);
            using (sb.OpenBrace($"namespace {entity.Namespace}"))
            {
                using (sb.OpenBrace($"public partial class {entity.Name}Metadata: IRepositoryMetadata<{entity.Name}>"))
                {
                    Metadata(sb, entity);  
                }
            }

            return sb.ToString();
        }

        static void Metadata(ScriptBuilder sb, SchemaEntity entity)
        {
            sb.WriteLine("// Data Access");
            sb.WriteLine();

            sb.WriteLine($"public static string DatabaseTable => \"{entity.Name}\";");
            sb.WriteLine();

            sb.WriteLine($"public static string DefaultOrderBy => \"{entity.DefaultOrderBy}\";");
            sb.WriteLine();

            sb.WriteLine($"public static {entity.Name} DataReader(SqlDataReader dr) => new {entity.Name}(dr);");
            sb.WriteLine();

            using (sb.OpenBrace($"public static List<SqlParameter> ExtractParameters({entity.Name} entity, IEnumerable<string> properties)"))
            {
                sb.WriteLine($"var result = new List<SqlParameter> {{ new SqlParameter(nameof({entity.Name}.Id), entity.Id) }};");
                
                using (sb.OpenBrace($"foreach(var property in properties)"))
                {
                    using (sb.OpenBrace($"switch (property)"))
                    {
                        foreach (var member in entity.Members)
                        {
                            sb.WriteLine($"case nameof({entity.Name}.{member.Name}):");
                            if(member.Name != "Id")
                                sb.WriteLine($"result.Add(new SqlParameter(nameof({entity.Name}.{member.Name}), {member.GenerateRepositoryGetter("entity.")}));");
                            sb.WriteLine("break;");
                        }
                    }
                }
                sb.WriteLine("return result;");
            }
            sb.WriteLine();

            using(sb.OpenBrace("public static string InsertSQL(IEnumerable<string>? properties = null)"))
            {
                sb.WriteLine("properties = properties ?? InsertProperties;");
                sb.WriteLine($"return RepositoryMetadataDefaults.InsertSQL<{entity.Name}, {entity.Name}Metadata>(properties);");
            }
            sb.WriteLine();

            using (sb.OpenBrace("public static string UpdateSQL(IEnumerable<string>? properties = null)"))
            {
                sb.WriteLine("properties = properties ?? UpdateProperties;");
                sb.WriteLine($"return RepositoryMetadataDefaults.UpdateSQL<{entity.Name}, {entity.Name}Metadata>(properties);");
            }
            sb.WriteLine();

            using (sb.OpenBrace("public static string DeleteSQL()"))
            {
                sb.WriteLine($"return RepositoryMetadataDefaults.DeleteSQL<{entity.Name}, {entity.Name}Metadata>();");
            }
            sb.WriteLine();

            using (sb.OpenBrace($"public static IEnumerable<string> SelectProperties"))
            {
                using (sb.OpenBrace("get"))
                {
                    foreach (var member in entity.Members)
                    {
                        sb.WriteLine($"yield return \"{member.Name}\";");
                    }
                }
            }
            sb.WriteLine();

            using (sb.OpenBrace($"public static IEnumerable<string> InsertProperties"))
            {
                using (sb.OpenBrace("get"))
                {
                    foreach (var member in entity.Members)
                    {
                        sb.WriteLine($"yield return \"{member.Name}\";");
                    }
                }
            }
            sb.WriteLine();

            using (sb.OpenBrace($"public static IEnumerable<string> UpdateProperties"))
            {
                using (sb.OpenBrace("get"))
                {
                    foreach (var member in entity.Members)
                    {
                        if (member.IsKey)
                            continue;
                        sb.WriteLine($"yield return \"{member.Name}\";");
                    }
                }
            }
            sb.WriteLine();

            sb.WriteLine("// Validation");
            sb.WriteLine();

            using (sb.OpenBrace($"public static void Validate({entity.Name} entity)"))
            {
                foreach (var member in entity.Members)
                    member.GenerateCoercion(sb);
                sb.WriteLine("Validator.ValidateAndThrow(entity);");
            }
            sb.WriteLine();

            sb.WriteLine($"public static {entity.Name}RepositoryValidator Validator {{ get; }} = new {entity.Name}RepositoryValidator();");
            sb.WriteLine();

            foreach (var member in entity.Members)
            {
                foreach (var metadata in member.GenerateValidationMetadata())
                {
                    sb.WriteLine(metadata);
                }
            }

            foreach (var member in entity.Members)
            {
                var rules = member.GenerateValidationRules().ToList();
                if (rules.Count > 0)
                {
                    sb.WriteLine();
                    using (sb.OpenBrace($"public static void {member.Name}_ValidationRules<T>(IRuleBuilder<T, {member.CLRTypeWithNullable}> ruleBuilder)"))
                    {
                        sb.WriteLine("ruleBuilder");

                        for (var i = 0; i < rules.Count; i++)
                        {
                            sb.WriteLine(rules[i] + (i == rules.Count - 1 ? ";" : ""));
                        }

                    }
                }
            }

            sb.WriteLine();
            using (sb.OpenBrace($"public class {entity.Name}RepositoryValidator: AbstractValidator<{entity.Name}>"))
            {
                using (sb.OpenBrace($"public {entity.Name}RepositoryValidator()"))
                {
                    foreach (var member in entity.Members)
                    {
                        if (member.GenerateValidationRules().Count() == 0)
                            continue;

                        sb.WriteLine($"RuleFor(x => x.{member.Name}).Use({member.Name}_ValidationRules);");
                    }
                }
            }
        }
    }
}
