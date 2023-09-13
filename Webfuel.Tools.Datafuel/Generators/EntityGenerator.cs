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
            File.WriteAllText(entity.GeneratedDirectory + $@"\{entity.Name}\{entity.Name}Accessor.cs", Accessor(entity));

            if (entity.Repository)
                RepositoryGenerator.GenerateRepository(entity);

            if (entity.Static)
                StaticGenerator.GenerateStatic(entity);
        }

        static string Entity(SchemaEntity entity)
        {
            var sb = new ScriptBuilder();

            Usings(sb, entity);
            using (sb.OpenBrace($"namespace {entity.Namespace}"))
            {
                using (sb.OpenBrace($"public partial class {entity.Name}{Interface(entity)}"))
                {
                    foreach (var member in entity.Members)
                        member.GenerateEntityProperty(sb);

                    Copy(sb, entity);
                }
            }

            return sb.ToString();
        }

        static void Usings(ScriptBuilder sb, SchemaEntity entity)
        {
            sb.WriteLine("using System;");
            sb.WriteLine("using System.Collections.Generic;");
            sb.WriteLine("using System.ComponentModel.DataAnnotations.Schema;");
            sb.WriteLine("");
        }

        static string Interface(SchemaEntity entity)
        {
            var result = String.Empty;

            if (entity.Audited)
                result += "IAudited";

            if(!String.IsNullOrEmpty(entity.Interface))
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

        static string Accessor(SchemaEntity entity)
        {
            var sb = new ScriptBuilder();

            Usings(sb, entity);
            using (sb.OpenBrace($"namespace {entity.Namespace}"))
            {
                using (sb.OpenBrace($"internal class {entity.Name}RepositoryAccessor: IRepositoryAccessor<{entity.Name}>"))
                {
                    sb.WriteLine($"public string DatabaseSchema => \"{Settings.DatabaseSchema}\";");
                    sb.WriteLine($"public string DatabaseTable => \"{entity.Name}\";");
                    sb.WriteLine($"public string DefaultOrderBy => \"{entity.DefaultOrderBy}\";");

                    using (sb.OpenBrace($"public object? GetValue({entity.Name} entity, string property)"))
                    {
                        using (sb.OpenBrace("switch(property)"))
                        {
                            foreach (var member in entity.Members)
                            {
                                sb.WriteLine($"case nameof({entity.Name}.{member.Name}):");
                                member.GenerateRepositoryGetter(sb);
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
                                member.GenerateRepositorySetter(sb);
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
                            member.GenerateValidation(sb);
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
