using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Webfuel.Tools.Typefuel
{
    public static class AngularEnumGenerator
    {
        public static void GenerateEnum(ApiSchema schema)
        {
            File.WriteAllText(Settings.ApiRoot + $@"\api.enums.ts", Static(schema));
        }

        static string Static(ApiSchema schema)
        {
            var sb = new ScriptBuilder();

            var complexTypes = EnumerateTypes(schema);
            if (complexTypes.Count > 0)
                sb.WriteLine($"import {{ { String.Join(", ", complexTypes.Select(p => AngularTypesGenerator.TypeName(p))) } }} from './api.types';");

            sb.WriteLine();

            foreach (var @static in schema.Enums.Values)
            {
                if (@static.IsEnum)
                {
                     StaticEnum(sb, @static);
                }
                else
                {
                    StaticClass(sb, @static);
                }
            }
            return sb.ToString().FormatScript();
        }

        static List<ApiComplexType> EnumerateTypes(ApiSchema schema)
        {
            var result = new List<ApiComplexType>();

            foreach (var @static in schema.Enums.Values)
            {
                if (@static.IsEnum)
                    continue;

                if (!result.Contains(@static.ValueType.Type) && @static.ValueType.Type is ApiComplexType)
                    result.Add(@static.ValueType.Type as ApiComplexType);
            }

            return result;
        }

        public static void StaticEnum(ScriptBuilder sb, ApiEnum @static)
        {
            using (sb.OpenBrace($"export enum {@static.Name}"))
            {
                foreach (var row in @static.Rows)
                {
                    sb.WriteLine($"{row.Name} = {FormatValue(row.Value)},");
                }
            }
            sb.WriteLine();
        }

        public static void StaticClass(ScriptBuilder sb, ApiEnum @static)
        {
            using (sb.OpenBrace($"export enum {@static.Name}"))
            {
                foreach (var row in @static.Rows)
                {
                    sb.WriteLine($"{row.Name} = {FormatValue(row.Value)},");
                }
            }
            sb.WriteLine();
        }

        static string FormatValue(object value)
        {
            if (value == null)
                return "null";

            if (value is Guid || value is String)
                return "\"" + value.ToString() + "\"";

            if (ApiTypeAnalysis.IsNumericType(value.GetType()))
                return value.ToString();

            throw new InvalidOperationException("Unable to format value");
        }
    }
}
