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

            File.WriteAllText(entity.GeneratedDirectory + $@"\{entity.Name}\{entity.Name}ReportMapper.cs", Reference(entity));
        }

        static string Reference(SchemaEntity entity)
        {
            var sb = new ScriptBuilder();

            Usings(sb);

            sb.Write($@"namespace Webfuel.Domain.StaticData
{{
    [Service(typeof(IReportMapper<{entity.Name}>))]
    internal class {entity.Name}ReportMapper : IReportMapper<{entity.Name}>
    {{
        private readonly I{entity.Name}Repository _repository;
        private readonly IStaticDataCache _staticDataCache;

        public {entity.Name}ReportMapper(I{entity.Name}Repository repository, IStaticDataCache staticDataCache)
        {{
            _repository = repository;
            _staticDataCache = staticDataCache;
        }}

        public async Task<object?> Get(Guid id)
        {{
            var staticData = await _staticDataCache.GetStaticData();
            return staticData.{entity.Name}.FirstOrDefault(x => x.Id == id);
        }}

        public async Task<QueryResult<ReferenceLookup>> Lookup(Query query)
        {{
            query.Contains(nameof({entity.Name}.Name), query.Search); 

            var result = await _repository.Query{entity.Name}(query);

            return new QueryResult<ReferenceLookup>
            {{
                TotalCount = result.TotalCount,
                Items = result.Items.Select(p => new ReferenceLookup
                {{
                    Id = p.Id,
                    Name = p.Name
                }}).ToList()
            }};
        }}

        public Guid Id(object reference)
        {{
            if (reference is not {entity.Name} entity)
                throw new Exception($""Cannot get id of type {{reference.GetType()}}"");
            return entity.Id;
        }}

        public string Name(object reference)
        {{
            if (reference is not {entity.Name} entity)
                throw new Exception($""Cannot get name of type {{reference.GetType()}}"");
            return entity.Name;
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


