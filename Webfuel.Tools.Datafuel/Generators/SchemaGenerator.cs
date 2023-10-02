using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Webfuel.Tools.Datafuel
{
    public static class SchemaGenerator
    {
        public static void Generate(Schema schema)
        {
            DeleteDirectory(schema);

            foreach (var entity in schema.Entities) { 
                EntityGenerator.GenerateEntity(entity);
                MetadataGenerator.GenerateMetadata(entity);
            }

            GenerateRepositoryRegistration(schema);

            DatabaseGenerator.GenerateDatabase(schema);
        }
        static void GenerateRepositoryRegistration(Schema schema)
        {
            var done = new List<String>();
            foreach (var entity in schema.Entities)
            {
                if (done.Contains(entity.Assembly))
                    continue;

                File.WriteAllText(entity.GeneratedDirectory + $@"\RepositoryRegistration.cs", RepositoryRegistration(schema, entity.Assembly));
            }
        }

        static string RepositoryRegistration(Schema schema, string assembly)
        {
            var sb = new ScriptBuilder();

            sb.WriteLine("using Microsoft.Extensions.DependencyInjection;");
            using (sb.OpenBrace($"namespace Webfuel{(assembly == "Core" ? String.Empty : "." + assembly)}"))
            {
                using (sb.OpenBrace("internal static class RepositoryRegistration"))
                using (sb.OpenBrace("public static void AddRepositoryServices(this IServiceCollection services)"))
                {
                    foreach (var entity in schema.Entities.Where(p => p.Repository && p.Assembly == assembly))
                    {
                        sb.WriteLine($"services.AddSingleton<I{entity.Name}Repository, {entity.Name}Repository>();");
                        sb.WriteLine($"services.AddSingleton<IRepositoryAccessor<{entity.Name}>, {entity.Name}RepositoryAccessor>();");
                        sb.WriteLine($"services.AddSingleton<IRepositoryMapper<{entity.Name}>, RepositoryDefaultMapper<{entity.Name}>>();");
                        sb.WriteLine();
                    }
                }
            }
            return sb.ToString();
        }

        static void DeleteDirectory(Schema schema)
        {
            foreach (var entity in schema.Entities)
                DeleteDirectory(entity.GeneratedDirectory);
        }

        static void DeleteDirectory(string directory)
        {
            if (!Directory.Exists(directory))
                throw new InvalidOperationException("Specified directory does not exist");

            for (var i = 0; i < 5; i++)
            {
                try
                {
                    if (Directory.Exists(directory) && Directory.EnumerateFileSystemEntries(directory).Count() > 0)
                    {
                        Thread.Sleep(500);
                        Directory.Delete(directory, true);
                    }
                }
                catch { /* GULP */ }
            }

            if (Directory.Exists(directory) && Directory.EnumerateFileSystemEntries(directory).Count() > 0)
                throw new InvalidOperationException("Unable to delete specified directory");

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            if (!Directory.Exists(directory))
                throw new InvalidOperationException("Unable to create specified directory");
        }
    }
}
