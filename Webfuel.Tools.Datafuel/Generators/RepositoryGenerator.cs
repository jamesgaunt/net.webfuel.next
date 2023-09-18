using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Webfuel.Tools.Datafuel
{
    static class RepositoryGenerator
    {
        public static void GenerateRepository(SchemaEntity entity)
        {
            File.WriteAllText(entity.GeneratedDirectory + $@"\{entity.Name}\{entity.Name}Repository.cs", Repository(entity));
        }

        static string Repository(SchemaEntity entity)
        {
            var sb = new ScriptBuilder();

            Usings(sb, entity);
            using (sb.OpenBrace($"namespace {entity.Namespace}"))
            {

                Interface(sb, entity);
                
                Implementation(sb, entity);
            }

            return sb.ToString();
        }

        static void Usings(ScriptBuilder sb, SchemaEntity entity)
        {
            sb.WriteLine("using System;");
            sb.WriteLine("using System.Linq;");
            sb.WriteLine("using System.Threading.Tasks;");
            sb.WriteLine("using System.Collections.Generic;");
            sb.WriteLine("using Microsoft.Data.SqlClient;");
            sb.WriteLine("");
        }

        static void Interface(ScriptBuilder sb, SchemaEntity entity)
        {
            using (sb.OpenBrace($"internal partial interface I{entity.Name}Repository"))
            {
                if (!entity.Static && !entity.ReadOnly)
                {
                    sb.WriteLine($"Task<{entity.Name}> Insert{entity.Name}Async({entity.Name} entity);");

                    if (entity.Key != null)
                    {
                        sb.WriteLine($"Task<{entity.Name}> Update{entity.Name}Async({entity.Name} entity);");
                        sb.WriteLine($"Task<{entity.Name}> Update{entity.Name}Async({entity.Name} entity, IEnumerable<string> properties);");

                        sb.WriteLine($"Task<{entity.Name}> Update{entity.Name}Async({entity.Name} updated, {entity.Name} original);");
                        sb.WriteLine($"Task<{entity.Name}> Update{entity.Name}Async({entity.Name} updated, {entity.Name} original, IEnumerable<string> properties);");

                        sb.WriteLine($"Task Delete{entity.Name}Async({entity.Key.CLRType} key);");
                    }
                }

                sb.WriteLine($"Task<QueryResult<{entity.Name}>> Query{entity.Name}Async(RepositoryQuery query);");

                foreach (var query in entity.Queries)
                    QueryInterface(sb, query);
            }
        }

        static void Implementation(ScriptBuilder sb, SchemaEntity entity)
        {
            using (sb.OpenBrace($"internal partial class {entity.Name}Repository: I{entity.Name}Repository"))
            {
                Constructor(sb, entity);

                if (!entity.Static && !entity.ReadOnly)
                {
                    Insert(sb, entity);
                    Update(sb, entity);
                    UpdateWithCompare(sb, entity);
                    Delete(sb, entity);
                }

                Query(sb, entity);
                foreach (var query in entity.Queries)
                    Query(sb, query);
            }
        }

        static void Constructor(ScriptBuilder sb, SchemaEntity entity)
        {
            sb.WriteLine($"private readonly IRepositoryService RepositoryService;");
            sb.WriteLine($"private readonly IRepositoryQueryService RepositoryQueryService;");
            using (sb.OpenBrace($"public {entity.Name}Repository(IRepositoryService repositoryService, IRepositoryQueryService repositoryQueryService)"))
            {
                sb.WriteLine("RepositoryService = repositoryService;");
                sb.WriteLine("RepositoryQueryService = repositoryQueryService;");
            }
        }

        static void Insert(ScriptBuilder sb, SchemaEntity entity)
        {
            using (sb.OpenBrace($"public async Task<{entity.Name}> Insert{entity.Name}Async({entity.Name} entity)"))
            {
                sb.WriteLine($"return await RepositoryService.ExecuteInsertAsync(entity);");
            }
        }

        static void Update(ScriptBuilder sb, SchemaEntity entity)
        {
            if (entity.Key == null)
                return;

            using (sb.OpenBrace($"public async Task<{entity.Name}> Update{entity.Name}Async({entity.Name} entity)"))
            {
                sb.WriteLine($"return await RepositoryService.ExecuteUpdateAsync(entity);");
            }
            using (sb.OpenBrace($"public async Task<{entity.Name}> Update{entity.Name}Async({entity.Name} entity, IEnumerable<string> properties)"))
            {
                sb.WriteLine($"return await RepositoryService.ExecuteUpdateAsync(entity, properties);");
            }
        }

        static void UpdateWithCompare(ScriptBuilder sb, SchemaEntity entity)
        {
            if (entity.Key == null)
                return;

            using (sb.OpenBrace($"public async Task<{entity.Name}> Update{entity.Name}Async({entity.Name} updated, {entity.Name} original)"))
            {
                sb.WriteLine($"if(updated.{entity.Key.Name} != original.{entity.Key.Name}) throw new InvalidOperationException(\"Update{entity.Name}Async: Entity keys do not match.\");");
                sb.WriteLine("var _properties = new List<string>();");
                foreach (var member in entity.Members)
                {
                    if (member == entity.Key)
                        continue;
                    sb.WriteLine($"if({member.GenerateComparisonGetter("updated")} != {member.GenerateComparisonGetter("original")}) _properties.Add(\"{member.Name}\");");
                }

                sb.WriteLine("if(_properties.Count == 0) return updated;");
                sb.WriteLine($"return await RepositoryService.ExecuteUpdateAsync(updated, _properties);");
            }

            using (sb.OpenBrace($"public async Task<{entity.Name}> Update{entity.Name}Async({entity.Name} updated, {entity.Name} original, IEnumerable<string> properties)"))
            {
                sb.WriteLine($"if(updated.{entity.Key.Name} != original.{entity.Key.Name}) throw new InvalidOperationException(\"Update{entity.Name}Async: Entity keys do not match.\");");
                sb.WriteLine("var _properties = new List<string>();");
                foreach (var member in entity.Members)
                {
                    if (member == entity.Key)
                        continue;
                    sb.WriteLine($"if(properties.Contains(\"{member.Name}\") && {member.GenerateComparisonGetter("updated")} != {member.GenerateComparisonGetter("original")}) _properties.Add(\"{member.Name}\");");
                }

                sb.WriteLine("if(_properties.Count == 0) return updated;");
                sb.WriteLine($"return await RepositoryService.ExecuteUpdateAsync(updated, _properties);");
            }
        }

        static void Delete(ScriptBuilder sb, SchemaEntity entity)
        {
            if (entity.Key == null)
                return;

            using (sb.OpenBrace($"public async Task Delete{entity.Name}Async({entity.Key.CLRType} key)"))
            {
                sb.WriteLine($"await RepositoryService.ExecuteDeleteAsync<{entity.Name}>(key);");
            }
        }

        static void QueryInterface(ScriptBuilder sb, SchemaEntityQuery query)
        {
            sb.WriteLine($"Task<{query.GenerateResultType()}> {query.Name}Async({query.GenerateSignature()});");

            // Hack in Require
            if (query.Shape == QueryShape.Get && query.Name.StartsWith("Get"))
            {
                sb.WriteLine($"Task<{query.Entity.Name}> Require{query.Name.Substring("Get".Length)}Async({query.GenerateSignature()});");
            }
        }

        static void Query(ScriptBuilder sb, SchemaEntity entity)
        {
            using (sb.OpenBrace($"public async Task<QueryResult<{entity.Name}>> Query{entity.Name}Async(RepositoryQuery query)"))
            {
                sb.WriteLine($"return await RepositoryQueryService.ExecuteQueryAsync(query, new {entity.Name}RepositoryAccessor());");
            }
        }

        static void Query(ScriptBuilder sb, SchemaEntityQuery query)
        {
            using (sb.OpenBrace($"public async Task<{query.GenerateResultType()}> {query.Name}Async({query.GenerateSignature()})"))
            {
                sb.WriteLine($"var sql = @\"{query.Sql.Trim().Replace("[dbo]", $"[{Settings.DatabaseSchema}]")}\";");
                if (query.Parameters.Count > 0)
                {
                    using (sb.OpenBrace("var parameters = new List<SqlParameter>", trailingSemicolon: true))
                    {
                        foreach (var parameter in query.Parameters)
                        {
                            sb.Write(parameter.Primative.ParameterMapper(parameter));
                            sb.WriteLine(",");
                        }
                    }
                }
                switch (query.Shape)
                {
                    case QueryShape.Get:
                        sb.WriteLine($"return (await RepositoryService.ExecuteReaderAsync<{query.Entity.Name}>(sql{(query.Parameters.Count > 0 ? ", parameters" : "")})).SingleOrDefault();");
                        break;
                    case QueryShape.Select:
                        sb.WriteLine($"return await RepositoryService.ExecuteReaderAsync<{query.Entity.Name}>(sql{(query.Parameters.Count > 0 ? ", parameters" : "")});");
                        break;
                    case QueryShape.Count:
                        sb.WriteLine($"return (int)((await RepositoryService.ExecuteScalarAsync(sql{(query.Parameters.Count > 0 ? ", parameters" : "")}))!);");
                        break;
                    case QueryShape.Unknown:
                        sb.WriteLine($"return await RepositoryService.ExecuteScalarAsync(sql{(query.Parameters.Count > 0 ? ", parameters" : "")});");
                        break;
                }
            }

            // Hack in Require
            if (query.Shape == QueryShape.Get && query.Name.StartsWith("Get"))
            {
                using (sb.OpenBrace($"public async Task<{query.Entity.Name}> Require{query.Name.Substring("Get".Length)}Async({query.GenerateSignature()})"))
                {
                    var result = sb.WriteLine($"return await {query.Name}Async({query.GenerateArguments()}) ?? throw new InvalidOperationException(\"The specified {query.Entity.Name} does not exist\");");
                }
            }
        }
    }
}
