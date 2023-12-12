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

            File.WriteAllText(entity.GeneratedDirectory + $@"\{entity.Name}\{entity.Name}ReportReferenceProvider.cs", Reference(entity));
        }

        static string Reference(SchemaEntity entity)
        {
            var sb = new ScriptBuilder();

            Usings(sb);

            sb.Write($@"namespace Webfuel.Domain.StaticData
{{
    [Service(typeof(IReportReferenceProvider<{entity.Name}>))]
    internal class {entity.Name}ReportReferenceProvider : IReportReferenceProvider<{entity.Name}>
    {{
        private readonly I{entity.Name}Repository _repository;
        private readonly IStaticDataCache _staticDataCache;

        public {entity.Name}ReportReferenceProvider(I{entity.Name}Repository repository, IStaticDataCache staticDataCache)
        {{
            _repository = repository;
            _staticDataCache = staticDataCache;
        }}

        public async Task<ReportReference?> Get(Guid id)
        {{
            var staticData = await _staticDataCache.GetStaticData();
            var entity = staticData.{entity.Name}.FirstOrDefault(x => x.Id == id);

            return entity == null ? null : new ReportReference
            {{
                Id = entity.Id,
                Name = entity.Name,
                Entity = entity
            }};
        }}

        public async Task<QueryResult<ReportReference>> Query(Query query)
        {{
            var result = await _repository.Query{entity.Name}(query);

            return new QueryResult<ReportReference>
            {{
                TotalCount = result.TotalCount,
                Items = result.Items.Select(x => new ReportReference
                {{
                    Id = x.Id,
                    Name = x.Name,
                    Entity = x
                }}).ToList()
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


