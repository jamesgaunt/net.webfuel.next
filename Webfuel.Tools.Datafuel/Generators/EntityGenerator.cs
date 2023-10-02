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
            File.WriteAllText(entity.GeneratedDirectory + $@"\{entity.Name}\{entity.Name}RepositoryAccessor.cs", Accessor(entity));
            File.WriteAllText(entity.GeneratedDirectory + $@"\{entity.Name}\{entity.Name}RepositoryValidation.cs", Validator(entity));

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

        static string Validator(SchemaEntity entity)
        {
            var sb = new ScriptBuilder();

            Usings(sb, entity);
            using (sb.OpenBrace($"namespace {entity.Namespace}"))
            {
                using (sb.OpenBrace($"internal class {entity.Name}RepositoryValidator: AbstractValidator<{entity.Name}>"))
                {
                    using (sb.OpenBrace($"public {entity.Name}RepositoryValidator()"))
                    {
                        foreach (var member in entity.Members)
                        {
                            if (member.GenerateValidationRules().Count() == 0)
                                continue;

                            sb.WriteLine($"RuleFor(x => x.{member.Name}).Use({entity.Name}RepositoryValidationRules.{member.Name});");
                        }
                    }
                }

                using (sb.OpenBrace($"public static class {entity.Name}RepositoryValidationRules"))
                {
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
                            using (sb.OpenBrace($"public static void {member.Name}<T>(IRuleBuilder<T, {member.CLRTypeWithNullable}> ruleBuilder)"))
                            {
                                sb.WriteLine("ruleBuilder");

                                for (var i = 0; i < rules.Count; i++)
                                {
                                    sb.WriteLine(rules[i] + (i == rules.Count - 1 ? ";" : ""));
                                }

                            }
                        }
                    }

                }
            }
            return sb.ToString();
        }

        static string Accessor(SchemaEntity entity)
        {
            var sb = new ScriptBuilder();

            Usings(sb, entity);
            using (sb.OpenBrace($"namespace {entity.Namespace}"))
            {
                using (sb.OpenBrace($"internal class {entity.Name}RepositoryAccessor: IRepositoryAccessor<{entity.Name}>"))
                {
                    sb.WriteLine($"private readonly {entity.Name}RepositoryValidator _validator = new {entity.Name}RepositoryValidator();");
                    sb.WriteLine($"public string DatabaseTable => \"{entity.Name}\";");
                    sb.WriteLine($"public string DefaultOrderBy => \"{entity.DefaultOrderBy}\";");

                    using (sb.OpenBrace($"public object? GetValue({entity.Name} entity, string property)"))
                    {
                        using (sb.OpenBrace("switch(property)"))
                        {
                            foreach (var member in entity.Members)
                            {
                                sb.WriteLine($"case nameof({entity.Name}.{member.Name}):");
                                sb.WriteLine("return " + member.GenerateRepositoryGetter() + ";");
                            }
                            sb.WriteLine("default: throw new InvalidOperationException($\"Unrecognised entity property {property}\");");
                        }
                    }

                    using (sb.OpenBrace($"public void SetValue({entity.Name} entity, string property, object? value)"))
                    {
                        using (sb.OpenBrace("switch(property)"))
                        {
                            foreach (var member in entity.Members)
                            {
                                sb.WriteLine($"case nameof({entity.Name}.{member.Name}):");
                                sb.WriteLine(member.GenerateRepositorySetter());
                                sb.WriteLine("break;");
                            }
                            // Ignore additional properties coming in from the database
                            // sb.WriteLine("default: throw new InvalidOperationException($\"Unrecognised entity property {property}\");");
                        }
                    }

                    using (sb.OpenBrace($"public {entity.Name} CreateInstance()"))
                    {
                        sb.WriteLine($"return new {entity.Name}();");
                    }

                    using (sb.OpenBrace($"public void Validate({entity.Name} entity)"))
                    {
                        foreach (var member in entity.Members)
                            member.GenerateCoercion(sb);
                        sb.WriteLine("_validator.ValidateAndThrow(entity);");
                    }


                    using (sb.OpenBrace($"public IEnumerable<string> InsertProperties"))
                    {
                        using (sb.OpenBrace("get"))
                        {
                            foreach (var member in entity.Members)
                            {
                                sb.WriteLine($"yield return \"{member.Name}\";");
                            }
                        }
                    }

                    using (sb.OpenBrace($"public IEnumerable<string> UpdateProperties"))
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
                }
            }

            return sb.ToString();
        }
    }
}
