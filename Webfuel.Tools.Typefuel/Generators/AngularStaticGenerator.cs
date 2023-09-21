using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Webfuel.Tools.Typefuel
{
    public static class AngularStaticGenerator
    {
        public static void GenerateStatic(ApiSchema schema)
        {
            File.WriteAllText(Settings.ApiRoot + $@"\api.static.ts", Static(schema));
        }

        static string Static(ApiSchema schema)
        {
            var sb = new ScriptBuilder();

            var complexTypes = EnumerateTypes(schema);
            if (complexTypes.Count > 0)
                sb.WriteLine($"import {{ { String.Join(", ", complexTypes.Select(p => AngularTypesGenerator.TypeName(p))) } }} from './api.types';");

            sb.WriteLine();

            foreach (var @static in schema.Static.Values)
            {
                if (@static.IsEnum)
                {
                     // StaticEnum(sb, @static);
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

            foreach (var @static in schema.Static.Values)
            {
                if (@static.IsEnum)
                    continue;

                if (!result.Contains(@static.ValueType.Type) && @static.ValueType.Type is ApiComplexType)
                    result.Add(@static.ValueType.Type as ApiComplexType);
            }

            return result;
        }

        public static void StaticEnum(ScriptBuilder sb, ApiStatic @static)
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

        public static void StaticClass(ScriptBuilder sb, ApiStatic @static)
        {
            using (sb.OpenBrace($"export class {@static.Name}"))
            {
                foreach (var row in @static.Rows)
                {
                    sb.WriteLine($"static readonly {row.Name} = {FormatValue(row.Value)};");
                }

                if (@static.ValueArray != null)
                {
                    sb.WriteLine();
                    sb.WriteLine($"static readonly values: {AngularTypesGenerator.TypeDescriptor(@static.ValueType)}[] = [");
                    var complexType = @static.ValueType.Type as ApiComplexType;

                    foreach (var value in @static.ValueArray)
                    {
                        sb.WriteLine("{");
                        for (var pi = 0; pi < complexType.Properties.Count; pi++)
                        {
                            var property = complexType.Properties[pi];
                            var propertyType = value.GetType().GetProperty(property.Name);
                            var propertyValue = propertyType.GetValue(value);
                            sb.WriteLine($"{property.JsonPropertyName}: {FormatValue(propertyValue)},");
                        }
                        sb.WriteLine("},");
                    }
                    sb.WriteLine("];");

                    var idType = complexType.Properties.Where(p => p.Name == "Id").Single();

                    sb.WriteLine();
                    sb.WriteLine($"static readonly map: {{ [id: { AngularTypesGenerator.TypeDescriptor(idType.TypeDescriptor) }]: {AngularTypesGenerator.TypeDescriptor(@static.ValueType)} }} = {{");
                    foreach (var value in @static.ValueArray)
                    {
                        sb.Write(FormatValue(value.GetType().GetProperty("Id").GetValue(value)) + ": ");
                        sb.WriteLine("{");
                        for (var pi = 0; pi < complexType.Properties.Count; pi++)
                        {
                            var property = complexType.Properties[pi];
                            var propertyType = value.GetType().GetProperty(property.Name);
                            var propertyValue = propertyType.GetValue(value);
                            sb.WriteLine($"{property.JsonPropertyName}: {FormatValue(propertyValue)},");
                        }
                        sb.WriteLine("},");
                    }
                    sb.WriteLine("};");

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
