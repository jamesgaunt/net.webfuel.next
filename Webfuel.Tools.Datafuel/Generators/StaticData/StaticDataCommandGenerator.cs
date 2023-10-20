using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Webfuel.Tools.Datafuel
{
    public static class StaticDataCommandGenerator
    {
        public static void GenerateStaticDataCommands(Schema schema)
        {
            foreach (var entity in schema.Entities)
            {
                if (!entity.StaticData)
                    continue;

                if (!Directory.Exists(entity.GeneratedDirectory + $@"\{entity.Name}\Commands"))
                    Directory.CreateDirectory(entity.GeneratedDirectory + $@"\{entity.Name}\Commands");

                if (!entity.ReadOnly)
                {
                    File.WriteAllText(entity.GeneratedDirectory + $@"\{entity.Name}\Commands\Create{entity.Name}.cs", Create(entity));
                    File.WriteAllText(entity.GeneratedDirectory + $@"\{entity.Name}\Commands\Update{entity.Name}.cs", Update(entity));
                    File.WriteAllText(entity.GeneratedDirectory + $@"\{entity.Name}\Commands\Delete{entity.Name}.cs", Delete(entity));
                    File.WriteAllText(entity.GeneratedDirectory + $@"\{entity.Name}\Commands\Sort{entity.Name}.cs", Sort(entity));
                }

                File.WriteAllText(entity.GeneratedDirectory + $@"\{entity.Name}\Commands\Query{entity.Name}.cs", Query(entity));
            }
        }
        static void Usings(ScriptBuilder sb, SchemaEntity entity)
        {
            sb.WriteLine("using MediatR;");
            sb.WriteLine();
        }

        // Create

        static string Create(SchemaEntity entity)
        {
            var sb = new ScriptBuilder();

            Usings(sb, entity);
            using (sb.OpenBrace("namespace Webfuel.Domain.StaticData"))
            {
                using (sb.OpenBrace($"public class Create{entity.Name}: IRequest<{entity.Name}>"))
                {
                    sb.WriteLine("public required string Name { get; set; }");
                    foreach (var property in entity.Properties)
                    {
                        if (property.Name == "Name" || property.Name == "SortOrder")
                            continue;

                        sb.WriteLine($"public {property.CLRTypeWithNullable} {property.Name} {{ get; set; }} = {property.CLRLiteral(property.ParseValue(property.Default))};");
                    }
                }

                using (sb.OpenBrace($"internal class Create{entity.Name}Handler : IRequestHandler<Create{entity.Name}, {entity.Name}>"))
                {
                    sb.WriteLine($"private readonly I{entity.Name}Repository _{entity.Name.ToCamelCase()}Repository;");
                    sb.WriteLine();

                    sb.Write($@"
                        public Create{entity.Name}Handler(I{entity.Name}Repository {entity.Name.ToCamelCase()}Repository)
                        {{
                            _{entity.Name.ToCamelCase()}Repository = {entity.Name.ToCamelCase()}Repository;
                        }}
");

                    sb.Write($@"
                        public async Task<{entity.Name}> Handle(Create{entity.Name} request, CancellationToken cancellationToken)
                        {{
                            return await _{entity.Name.ToCamelCase()}Repository.Insert{entity.Name}(new {entity.Name} {{ 
                                Name = request.Name,
                    ");

                    foreach (var property in entity.Properties)
                    {
                        if (property.Name == "Name" || property.Name == "SortOrder")
                            continue;

                        sb.WriteLine($"{property.Name} = request.{property.Name},");
                    }
                    sb.Write($@"SortOrder = await _{entity.Name.ToCamelCase()}Repository.Count{entity.Name}(),
                            }});
                        }}
");
                }
            }
            return sb.ToString();
        }

        // Update

        static string Update(SchemaEntity entity)
        {
            var sb = new ScriptBuilder();

            Usings(sb, entity);
            using (sb.OpenBrace("namespace Webfuel.Domain.StaticData"))
            {
                using (sb.OpenBrace($"public class Update{entity.Name}: IRequest<{entity.Name}>"))
                {
                    sb.WriteLine("public required Guid Id { get; set; }");
                    sb.WriteLine("public required string Name { get; set; }");
                    foreach (var property in entity.Properties)
                    {
                        if (property.Name == "Name" || property.Name == "SortOrder")
                            continue;

                        sb.WriteLine($"public {property.CLRTypeWithNullable} {property.Name} {{ get; set; }} = {property.CLRLiteral(property.ParseValue(property.Default))};");
                    }
                }

                using (sb.OpenBrace($"internal class Update{entity.Name}Handler : IRequestHandler<Update{entity.Name}, {entity.Name}>"))
                {
                    sb.WriteLine($"private readonly I{entity.Name}Repository _{entity.Name.ToCamelCase()}Repository;");
                    sb.WriteLine();

                    sb.Write($@"
                        public Update{entity.Name}Handler(I{entity.Name}Repository {entity.Name.ToCamelCase()}Repository)
                        {{
                            _{entity.Name.ToCamelCase()}Repository = {entity.Name.ToCamelCase()}Repository;
                        }}
");

                    sb.Write($@"
                        public async Task<{entity.Name}> Handle(Update{entity.Name} request, CancellationToken cancellationToken)
                        {{
                            var original = await _{entity.Name.ToCamelCase()}Repository.Require{entity.Name}(request.Id);

                            var updated = original.Copy();
                            updated.Name = request.Name;
                     ");


                    foreach (var property in entity.Properties)
                    {
                        if (property.Name == "Name" || property.Name == "SortOrder")
                            continue;

                        sb.WriteLine($"updated.{property.Name} = request.{property.Name};");
                    }

                    sb.Write($@"
                            return await _{entity.Name.ToCamelCase()}Repository.Update{entity.Name}(original: original, updated: updated); 
                        }}
");
                }
            }
            return sb.ToString();
        }

        // Delete

        static string Delete(SchemaEntity entity)
        {
            var sb = new ScriptBuilder();

            Usings(sb, entity);
            using (sb.OpenBrace("namespace Webfuel.Domain.StaticData"))
            {
                using (sb.OpenBrace($"public class Delete{entity.Name}: IRequest"))
                {
                    sb.WriteLine("public required Guid Id { get; set; }");
                }

                using (sb.OpenBrace($"internal class Delete{entity.Name}Handler : IRequestHandler<Delete{entity.Name}>"))
                {
                    sb.WriteLine($"private readonly I{entity.Name}Repository _{entity.Name.ToCamelCase()}Repository;");
                    sb.WriteLine();

                    sb.Write($@"
                        public Delete{entity.Name}Handler(I{entity.Name}Repository {entity.Name.ToCamelCase()}Repository)
                        {{
                            _{entity.Name.ToCamelCase()}Repository = {entity.Name.ToCamelCase()}Repository;
                        }}
");

                    sb.Write($@"
                        public async Task Handle(Delete{entity.Name} request, CancellationToken cancellationToken)
                        {{
                            await _{entity.Name.ToCamelCase()}Repository.Delete{entity.Name}(request.Id);
                        }}
");
                }
            }
            return sb.ToString();
        }

        // Query

        static string Query(SchemaEntity entity)
        {
            var sb = new ScriptBuilder();

            Usings(sb, entity);
            using (sb.OpenBrace("namespace Webfuel.Domain.StaticData"))
            {
                using (sb.OpenBrace($"public class Query{entity.Name}: Query, IRequest<QueryResult<{entity.Name}>>"))
                {
                    sb.Write($@"
                        public Query ApplyCustomFilters()
                        {{
                            this.Contains(nameof({entity.Name}.Name), Search);
                            return this;
                        }}
");
                }

                using (sb.OpenBrace($"internal class Query{entity.Name}Handler : IRequestHandler<Query{entity.Name}, QueryResult<{entity.Name}>>"))
                {
                    sb.WriteLine($"private readonly I{entity.Name}Repository _{entity.Name.ToCamelCase()}Repository;");
                    sb.WriteLine();

                    sb.Write($@"
                        public Query{entity.Name}Handler(I{entity.Name}Repository {entity.Name.ToCamelCase()}Repository)
                        {{
                            _{entity.Name.ToCamelCase()}Repository = {entity.Name.ToCamelCase()}Repository;
                        }}
");

                    sb.Write($@"
                        public async Task<QueryResult<{entity.Name}>> Handle(Query{entity.Name} request, CancellationToken cancellationToken)
                        {{
                            return await _{entity.Name.ToCamelCase()}Repository.Query{entity.Name}(request.ApplyCustomFilters());
                        }}
");
                }
            }
            return sb.ToString();
        }

        // Sort

        static string Sort(SchemaEntity entity)
        {
            var sb = new ScriptBuilder();

            Usings(sb, entity);
            using (sb.OpenBrace("namespace Webfuel.Domain.StaticData"))
            {
                using (sb.OpenBrace($"public class Sort{entity.Name}: IRequest"))
                {
                    sb.WriteLine("public required IEnumerable<Guid> Ids { get; set; }");
                }

                using (sb.OpenBrace($"internal class Sort{entity.Name}Handler : IRequestHandler<Sort{entity.Name}>"))
                {
                    sb.WriteLine($"private readonly I{entity.Name}Repository _{entity.Name.ToCamelCase()}Repository;");
                    sb.WriteLine();

                    sb.Write($@"
                        public Sort{entity.Name}Handler(I{entity.Name}Repository {entity.Name.ToCamelCase()}Repository)
                        {{
                            _{entity.Name.ToCamelCase()}Repository = {entity.Name.ToCamelCase()}Repository;
                        }}
");

                    sb.Write($@"
                        public async Task Handle(Sort{entity.Name} request, CancellationToken cancellationToken)
                        {{
                            var items = await _{entity.Name.ToCamelCase()}Repository.Select{entity.Name}();

                            var index = 0;
                            foreach (var id in request.Ids)
                            {{
                                var original = items.FirstOrDefault(p => p.Id == id);
                                if (original != null && original.SortOrder != index)
                                {{
                                    var updated = original.Copy();
                                    updated.SortOrder = index;
                                    await _{entity.Name.ToCamelCase()}Repository.Update{entity.Name}(updated: updated, original: original);
                                }}
                                index++;
                            }}
                        }}
");
                }
            }
            return sb.ToString();
        }


    }
}
