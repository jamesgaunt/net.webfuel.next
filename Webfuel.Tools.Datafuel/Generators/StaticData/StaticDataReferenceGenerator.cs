using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Webfuel.Tools.Datafuel
{
    public static class StaticDataReferenceProviderGenerator
    {


        public static void GenerateStaticDataReferenceProvider(SchemaEntity entity)
        {

            File.WriteAllText(entity.GeneratedDirectory + $@"\{entity.Name}\{entity.Name}ReferenceProvider.cs", Reference(entity));
        }

        static string Reference(SchemaEntity entity)
        {
            var sb = new ScriptBuilder();

            Usings(sb);

            sb.Write($@"namespace Webfuel.Domain.StaticData
{{
    public interface I{ entity.Name }ReferenceProvider: IReportReferenceProvider
    {{
    }}

    [Service(typeof(I{entity.Name}ReferenceProvider))]
    internal class {entity.Name}ReferenceProvider : I{entity.Name}ReferenceProvider
    {{
        private readonly I{entity.Name}Repository _repository;
        private readonly IStaticDataCache _staticDataCache;

        public {entity.Name}ReferenceProvider(I{entity.Name}Repository repository, IStaticDataCache staticDataCache)
        {{
            _repository = repository;
            _staticDataCache = staticDataCache;
        }}

        public async Task<ReportReference?> GetReference(Guid id)
        {{
            var staticData = await _staticDataCache.GetStaticData();
            var item = staticData.{entity.Name}.FirstOrDefault(x => x.Id == id);
            if (item == null)
                return null;

            return new ReportReference
            {{
                Id = item.Id,
                Name = item.Name,
            }};
        }}

        public async Task<QueryResult<ReportReference>> QueryReference(Query query)
        {{
            var result = await _repository.Query{entity.Name}(query);

            return new QueryResult<ReportReference>
            {{
                TotalCount = result.TotalCount,
                Items = result.Items.Select(x => new ReportReference
                {{
                    Id = x.Id,
                    Name = x.Name,
                }}).ToList(),
            }};
        }}
    }}
}}");
            return sb.ToString();
        }

        static void Usings(ScriptBuilder sb)
        {
            sb.WriteLine("using Webfuel.Reporting;");
            sb.WriteLine();
        }
    }
}


