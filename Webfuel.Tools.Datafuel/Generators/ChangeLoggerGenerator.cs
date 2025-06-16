namespace Webfuel.Tools.Datafuel;

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
            using (sb.OpenBrace($"internal partial class {entity.Name}ChangeLogService: I{entity.Name}ChangeLogService"))
            {
                using (sb.OpenBrace(@$"public async Task<string> GenerateChangeLog({entity.Name} original, {entity.Name} updated, string delimiter = ""\n"")"))
                {
                    sb.WriteLine(@"var staticData = await _staticDataService.GetStaticData();");
                    sb.WriteLine(@"var sb = new StringBuilder();");
                    sb.WriteLine();

                    foreach (var member in entity.Members)
                    {
                        if (!MemberIncluded(member))
                            continue;

                        var renderer = MemberRenderer(member);
                        if (renderer == null)
                            continue;

                        using (sb.OpenBrace($@"if({member.GenerateComparisonGetter("original")} != {member.GenerateComparisonGetter("updated")})"))
                        {
                            renderer.Render(sb);
                        }
                    }

                    sb.WriteLine(@"return sb.ToString();");
                }
            }
        }

        return sb.ToString();
    }

    public static bool MemberIncluded(SchemaEntityMember member)
    {
        if (member.IsKey)
            return false;

        if (member.TagParameter("ChangeLog") == "Ignore")
            return false;

        return true;
    }

    public static string MemberName(SchemaEntityMember member)
    {
        var name = member.TagParameter("ChangeLogName");
        if (!String.IsNullOrEmpty(name))
            return name;

        name = SplitCamelCase(member.Name);

        if (name.EndsWith(" Id"))
            name = name.Substring(0, name.Length - 3);

        if (name.EndsWith(" Ids"))
            name = name.Substring(0, name.Length - 4);

        return name;
    }

    public static ChangeLogRenderer? MemberRenderer(SchemaEntityMember member)
    {
        if (member is SchemaEntityReference reference && reference.ReferenceEntity.StaticData)
            return new StaticDataReferenceChangeLogRenderer { Member = member, StaticDataType = reference.ReferenceEntity };

        if (member is SchemaEntityReference userReference && userReference.ReferenceEntity.Name == "User")
            return new UserReferenceChangeLogRenderer { Member = member };

        if (member is SchemaEntityPropertyJson json && member.TagParameter("ChangeLog").EndsWith("[]"))
            return new StaticDataReferenceListChangeLogRenderer { Member = member, StaticDataType = json.TagParameter("ChangeLog").Substring(0, json.TagParameter("ChangeLog").Length - 2) };

        return new PrimativeChangeLogRenderer { Member = member };
    }

    public static string SplitCamelCase(string input)
    {
        var output = new List<char>();

        for (int i = 0; i < input.Length; i++)
        {
            if (i == 0)
            {
                output.Add(char.ToUpper(input[i]));
            }
            else
            {
                if (char.IsUpper(input[i]) && !char.IsUpper(input[i - 1]))
                {
                    output.Add(' ');
                }
                output.Add(input[i]);
            }
        }

        return new string(output.ToArray());
    }
}

abstract class ChangeLogRenderer
{
    public string Original { get; set; } = "original";

    public string Updated { get; set; } = "updated";

    public required SchemaEntityMember Member { get; init; }

    public abstract void Render(ScriptBuilder sb);
}

class PrimativeChangeLogRenderer : ChangeLogRenderer
{
    public override void Render(ScriptBuilder sb)
    {
        if (Member.Nullable)
            sb.WriteLine($@"sb.Append(""{ChangeLoggerGenerator.MemberName(Member)}: "").Append({Original}.{Member.Name}?.ToString() ?? ""NULL"").Append("" -> "").Append({Updated}.{Member.Name}?.ToString() ?? ""NULL"").Append(delimiter);");
        else
            sb.WriteLine($@"sb.Append(""{ChangeLoggerGenerator.MemberName(Member)}: "").Append({Original}.{Member.Name}).Append("" -> "").Append({Updated}.{Member.Name}).Append(delimiter);");
    }
}

class StaticDataReferenceChangeLogRenderer : ChangeLogRenderer
{
    public required SchemaEntity StaticDataType { get; init; }

    public override void Render(ScriptBuilder sb)
    {
        if (Member.Nullable)
        {
            sb.WriteLine($@"var o = {Original}.{Member.Name}.HasValue ? (await _staticDataService.Get{StaticDataType.Name}({Original}.{Member.Name}.Value))?.Name ?? ""UNKNOWN"" : ""NULL"";");
            sb.WriteLine($@"var u = {Updated}.{Member.Name}.HasValue ? (await _staticDataService.Get{StaticDataType.Name}({Updated}.{Member.Name}.Value))?.Name ?? ""UNKNOWN"" : ""NULL"";");
        }
        else
        {
            sb.WriteLine($@"var o = (await _staticDataService.Get{StaticDataType.Name}({Original}.{Member.Name}))?.Name ?? ""UNKNOWN"";");
            sb.WriteLine($@"var u = (await _staticDataService.Get{StaticDataType.Name}({Updated}.{Member.Name}))?.Name ?? ""UNKNOWN"";");
        }

        sb.WriteLine($@"sb.Append(""{ChangeLoggerGenerator.MemberName(Member)}: "").Append(o).Append("" -> "").Append(u).Append(delimiter);");
    }
}

class UserReferenceChangeLogRenderer : ChangeLogRenderer
{
    public override void Render(ScriptBuilder sb)
    {
        if (Member.Nullable)
        {
            sb.WriteLine($@"var o = {Original}.{Member.Name}.HasValue ? (await _userRepository.GetUser({Original}.{Member.Name}.Value))?.FullName ?? ""UNKNOWN"" : ""NULL"";");
            sb.WriteLine($@"var u = {Updated}.{Member.Name}.HasValue ? (await _userRepository.GetUser({Updated}.{Member.Name}.Value))?.FullName ?? ""UNKNOWN"" : ""NULL"";");
        }
        else
        {
            sb.WriteLine($@"var o = (await _userRepository.GetUser({Original}.{Member.Name}))?.FullName ?? ""UNKNOWN"";");
            sb.WriteLine($@"var u = (await _userRepository.GetUser({Updated}.{Member.Name}))?.FullName ?? ""UNKNOWN"";");
        }

        sb.WriteLine($@"sb.Append(""{ChangeLoggerGenerator.MemberName(Member)}: "").Append(o).Append("" -> "").Append(u).Append(delimiter);");
    }
}

class StaticDataReferenceListChangeLogRenderer : ChangeLogRenderer
{
    public required string StaticDataType { get; init; }

    public override void Render(ScriptBuilder sb)
    {
        sb.WriteLine($@"var o = string.Join("", "", {Original}.{Member.Name}.Select(async p => (await _staticDataService.Get{StaticDataType}(p))?.Name ?? ""UNKNOWN""));");
        sb.WriteLine($@"var u = string.Join("", "", {Updated}.{Member.Name}.Select(async p => (await _staticDataService.Get{StaticDataType}(p))?.Name ?? ""UNKNOWN""));");

        sb.WriteLine($@"sb.Append(""{ChangeLoggerGenerator.MemberName(Member)}: "").Append(o).Append("" -> "").Append(u).Append(delimiter);");
    }
}
