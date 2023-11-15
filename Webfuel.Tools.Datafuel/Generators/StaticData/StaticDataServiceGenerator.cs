using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Webfuel.Tools.Datafuel
{
    public static class StaticDataServiceGenerator
    {
        public static void GenerateStaticDataService(Schema schema)
        {
            File.WriteAllText(Settings.SolutionRoot + $@"\Webfuel.Domain.StaticData\_Generated\StaticDataService.cs", Model(schema));
        }

        static string Model(Schema schema)
        {
            var sb = new ScriptBuilder();

            Usings(sb);
            using (sb.OpenBrace($"namespace Webfuel.Domain.StaticData"))
            {
                using (sb.OpenBrace($"public partial interface IStaticDataService"))
                {
                    foreach (var entity in schema.Entities.Where(p => p.StaticData))
                    {
                        sb.WriteLine($@"Task<IReadOnlyList<{entity.Name}>> Select{entity.Name}();");
                        sb.WriteLine($@"Task<{entity.Name}?> Get{entity.Name}(Guid id);");
                        sb.WriteLine($@"Task<{entity.Name}> Require{entity.Name}(Guid id);");
                    }
                }

                using (sb.OpenBrace($"internal partial class StaticDataService: IStaticDataService"))
                {
                    foreach (var entity in schema.Entities.Where(p => p.StaticData))
                    {
                        using (sb.OpenBrace($@"public async Task<IReadOnlyList<{entity.Name}>> Select{entity.Name}()"))
                        {
                            sb.WriteLine($@"return (await GetStaticData()).{entity.Name};");
                        }
                        sb.WriteLine();
                        using (sb.OpenBrace($@"public async Task<{entity.Name}?> Get{entity.Name}(Guid id)"))
                        {
                            sb.WriteLine($@"return (await GetStaticData()).{entity.Name}.FirstOrDefault(p => p.Id == id);");
                        }
                        sb.WriteLine();
                        using (sb.OpenBrace($@"public async Task<{entity.Name}> Require{entity.Name}(Guid id)"))
                        {
                            sb.WriteLine($@"return (await GetStaticData()).{entity.Name}.First(p => p.Id == id);");
                        }
                        sb.WriteLine();
                    }
                }

            }
            return sb.ToString();
        }

        static void Usings(ScriptBuilder sb)
        {
            sb.WriteLine("using System;");
            sb.WriteLine();
        }
    }
}


