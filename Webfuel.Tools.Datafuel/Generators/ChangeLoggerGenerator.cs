using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Tools.Datafuel
{
    static class ChangeLoggerGenerator
    {

        public static void GenerateChangeLogger(SchemaEntity entity)
        {
            Directory.CreateDirectory(entity.GeneratedDirectory + @"\" + entity.Name);

            File.WriteAllText(entity.GeneratedDirectory + $@"\{entity.Name}\{entity.Name}ChangeLogService.cs", ChangeLogger(entity));
        }

        static void Usings(ScriptBuilder sb, SchemaEntity entity)
        {
            sb.WriteLine("using System;");
            sb.WriteLine("using System.Text;");
            sb.WriteLine("using Webfuel.Domain.StaticData;");
            sb.WriteLine("");
        }

        static string ChangeLogger(SchemaEntity entity)
        {
            var sb = new ScriptBuilder();

            Usings(sb, entity);
            using (sb.OpenBrace($"namespace {entity.Namespace}"))
            {
                using(sb.OpenBrace($"public partial interface I{entity.Name}ChangeLogService"))
                {

                }
                sb.WriteLine();

                sb.WriteLine($"[Service(typeof(I{entity.Name}ChangeLogService))]");
                using (sb.OpenBrace($"internal partial class {entity.Name}ChangeLogService: I{entity.Name}ChangeLogService"))
                {
                    sb.WriteLine("private readonly IStaticDataService _staticDataService;");
                    sb.WriteLine("private readonly IUserRepository _userRepository;");
                    sb.WriteLine();

                    using (sb.OpenBrace($"public {entity.Name}ChangeLogService(IStaticDataService staticDataService, IUserRepository userRepository)"))
                    {
                        sb.WriteLine("_staticDataService = staticDataService;");
                        sb.WriteLine("_userRepository = userRepository;");
                    }
                    sb.WriteLine();

                    using (sb.OpenBrace(@$"public async Task<string> GenerateChangeLog({entity.Name} original, {entity.Name} updated, string delimiter = ""\n"")"))
                    {
                        sb.WriteLine(@"var staticData = await _staticDataService.GetStaticData();");
                        sb.WriteLine(@"var sb = new StringBuilder();");
                        sb.WriteLine();

                        sb.WriteLine(@"return sb.ToString();");
                    }
                }
            }

            return sb.ToString();
        }
    }
}
