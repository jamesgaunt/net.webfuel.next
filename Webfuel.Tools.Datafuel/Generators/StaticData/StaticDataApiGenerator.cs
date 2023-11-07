using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Webfuel.Tools.Datafuel
{
    public static class StaticDataApiGenerator
    {
        public static void GenerateStaticDataApi(Schema schema)
        {
            foreach (var entity in schema.Entities)
            {
                if (!entity.StaticData)
                    continue;

                if (!Directory.Exists(Settings.ApiGeneratedRoot + $@"\Api"))
                    Directory.CreateDirectory(Settings.ApiGeneratedRoot + $@"\Api");

                File.WriteAllText(Settings.ApiGeneratedRoot + $@"\Api\{entity.Name}Api.cs", Api(entity));
            }
        }

        static string Api(SchemaEntity entity)
        {
            var sb = new ScriptBuilder();

            Usings(sb, entity);
            using (sb.OpenBrace($"namespace Webfuel.App"))
            {
                ApiService(sb, entity);
            }

            return sb.ToString();
        }

        static void Usings(ScriptBuilder sb, SchemaEntity entity)
        {
            sb.WriteLine(@"using MediatR;");
            sb.WriteLine(@"using Microsoft.AspNetCore.Mvc;");
            sb.WriteLine(@"using Webfuel.Domain.StaticData;");
            sb.WriteLine();
        }
        static void ApiService(ScriptBuilder sb, SchemaEntity entity)
        {
            sb.WriteLine($"[ApiService]");
            sb.WriteLine($"[ApiDataSource]");
            sb.WriteLine($"[ApiStaticData]");
            using (sb.OpenBrace($"public static class {entity.Name}Api"))
            {
                RegisterEndpoints(sb, entity);
                if (!entity.ReadOnly)
                {
                    Create(sb, entity);
                    Update(sb, entity);
                    Sort(sb, entity);
                    Delete(sb, entity);
                }
                Query(sb, entity);
            }
        }

        static void RegisterEndpoints(ScriptBuilder sb, SchemaEntity entity)
        {
            using (sb.OpenBrace($"public static void RegisterEndpoints(IEndpointRouteBuilder app)"))
            {
                if (!entity.ReadOnly)
                {

                    sb.Write(@$"
            // Commands

            app.MapPost(""api/{entity.Name.ToSnakeCase('-')}"", Create)
                .RequireClaim(c => c.UserGroupClaims.CanEditStaticData);

            app.MapPut(""api/{entity.Name.ToSnakeCase('-')}"", Update)
                .RequireClaim(c => c.UserGroupClaims.CanEditStaticData);

            app.MapPut(""api/{entity.Name.ToSnakeCase('-')}/sort"", Sort)
                .RequireClaim(c => c.UserGroupClaims.CanEditStaticData);

            app.MapDelete(""api/{entity.Name.ToSnakeCase('-')}/{{id:guid}}"", Delete)
                .RequireClaim(c => c.UserGroupClaims.CanEditStaticData);
            ");
                }

                sb.Write(@$"
            // Querys

            app.MapPost(""api/{entity.Name.ToSnakeCase('-')}/query"", Query)
                .RequireIdentity();


");
            }
        }

        static void Create(ScriptBuilder sb, SchemaEntity entity)
        {
            sb.Write(@$"
        public static Task<{entity.Name}> Create([FromBody] Create{entity.Name} command, IMediator mediator)
        {{
            return mediator.Send(command);
        }}
");
        }

        static void Update(ScriptBuilder sb, SchemaEntity entity)
        {
            sb.Write(@$"
        public static Task<{entity.Name}> Update([FromBody] Update{entity.Name} command, IMediator mediator)
        {{
            return mediator.Send(command);
        }}
");
        }

        static void Sort(ScriptBuilder sb, SchemaEntity entity)
        {
            sb.Write(@$"
        public static Task Sort([FromBody] Sort{entity.Name} command, IMediator mediator)
        {{
            return mediator.Send(command);
        }}
");
        }

        static void Delete(ScriptBuilder sb, SchemaEntity entity)
        {
            sb.Write(@$"
        public static Task Delete(Guid id, IMediator mediator)
        {{
            return mediator.Send(new Delete{entity.Name} {{ Id = id }});
        }}
");
        }

        static void Query(ScriptBuilder sb, SchemaEntity entity)
        {
            sb.Write(@$"
        public static Task<QueryResult<{entity.Name}>> Query([FromBody] Query{entity.Name} command, IMediator mediator)
        {{
            return mediator.Send(command);
        }}
");
        }
    }
}
