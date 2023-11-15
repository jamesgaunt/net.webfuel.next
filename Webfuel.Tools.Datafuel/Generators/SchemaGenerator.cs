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
            foreach (var entity in schema.Entities)
                DeleteDirectory(entity.GeneratedDirectory);

            DeleteDirectory(Settings.ApiGeneratedRoot);

            foreach (var entity in schema.Entities)
            {
                EntityGenerator.GenerateEntity(entity);
                MetadataGenerator.GenerateMetadata(entity);
            }

            // Static Data Features
            {
                StaticDataApiGenerator.GenerateStaticDataApi(schema);
                StaticDataCommandGenerator.GenerateStaticDataCommands(schema);

                StaticDataModelGenerator.GenerateStaticDataModel(schema);
                StaticDataServiceGenerator.GenerateStaticDataService(schema);
            }

            DatabaseGenerator.GenerateDatabase(schema);
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
