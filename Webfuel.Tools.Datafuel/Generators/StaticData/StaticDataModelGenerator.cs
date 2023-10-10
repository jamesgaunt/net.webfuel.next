using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Webfuel.Tools.Datafuel
{
    public static class StaticDataModelGenerator
    {
        public static void GenerateStaticDataModel(Schema schema)
        {
            File.WriteAllText(Settings.SolutionRoot + $@"\Webfuel.Domain.StaticData\_Generated\StaticDataModel.cs", Model(schema));
        }

        static string Model(Schema schema)
        {
            var sb = new ScriptBuilder();

            Usings(sb);
            using (sb.OpenBrace($"namespace Webfuel.Domain.StaticData"))
            {
                using (sb.OpenBrace($"public interface IStaticDataModel"))
                {
                    foreach (var entity in schema.Entities.Where(p => p.StaticData))
                    {
                        sb.WriteLine($"IReadOnlyList<{entity.Name}> {entity.Name} {{ get; }}");
                    }
                    sb.WriteLine("DateTimeOffset LoadedAt { get; }");
                }

                using (sb.OpenBrace($"internal class StaticDataModel: IStaticDataModel"))
                {
                    foreach (var entity in schema.Entities.Where(p => p.StaticData))
                    {
                        sb.WriteLine($"public required IReadOnlyList<{entity.Name}> {entity.Name} {{ get; init; }}");
                    }
                    sb.WriteLine("public DateTimeOffset LoadedAt { get; init; }");

                    sb.WriteLine();
                    using (sb.OpenBrace("internal static async Task<StaticDataModel> Load(IServiceProvider serviceProvider)"))
                    {
                        sb.WriteLine("return new StaticDataModel");
                        sb.WriteLine("{");
                        {
                            foreach (var entity in schema.Entities.Where(p => p.StaticData))
                            {
                                sb.WriteLine($"{entity.Name} = await serviceProvider.GetRequiredService<I{entity.Name}Repository>().Select{entity.Name}(),");
                            }
                            sb.WriteLine("LoadedAt = DateTimeOffset.Now");
                        }
                        sb.WriteLine("};");
                    }
                }

            }
            return sb.ToString();
        }

        static void Usings(ScriptBuilder sb)
        {
            sb.WriteLine("using Microsoft.Extensions.DependencyInjection;");
            sb.WriteLine();
        }
    }
}


