using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Webfuel.Tools.Typefuel
{
    public static class AngularTypesGenerator
    {
        public static void GenerateTypes(ApiSchema schema)
        {
            File.WriteAllText(Settings.ApiRoot + $@"\api.types.ts", Types(schema));
        }

        static string Types(ApiSchema schema)
        {
            var sb = new ScriptBuilder();

            foreach (var @static in schema.Static.Values)
            {
                if (@static.IsEnum)
                    AngularStaticGenerator.StaticEnum(sb, @static);
            }

            foreach (var type in schema.TypeContext.TypeMapping.Values)
                TypeDeclaration(sb, type);

            return sb.ToString().FormatScript();
        }

        public static string TypeName(ApiComplexType type)
        {
            if (!String.IsNullOrEmpty(type.Signature))
                return type.Signature;

            return (type.IsInterface ? "" : "I") + type.Name;
        }

        static void TypeDeclaration(ScriptBuilder sb, ApiType type)
        {
            // We don't need to declare primative types
            if (type is ApiPrimativeType)
                return;

            else if (type is ApiComplexType)
                TypeDeclaration(sb, type as ApiComplexType);

            else
                return;
        }

        static void TypeDeclaration(ScriptBuilder sb, ApiComplexType type)
        {
            if (!String.IsNullOrEmpty(type.Signature))
                return; // This type has a custom signature

            var declaration = new ScriptBuilder();

            declaration.Write($"export interface {TypeName(type)}");

            // Generic Type Parameters
            if (type.Parameters.Count > 0)
            {
                declaration.Write("<");
                for (var i = 0; i < type.Parameters.Count; i++)
                {
                    if (i > 0)
                        declaration.Write(", ");
                    declaration.Write(type.Parameters[i].Name);

                    // Extends
                    if (type.Parameters[i].GenericParameterConstraints.Count > 0)
                    {
                        declaration.Write(" extends ");
                        for (var j = 0; j < type.Parameters[i].GenericParameterConstraints.Count; j++)
                        {
                            if (j > 0)
                                declaration.Write(" & ");
                            TypeDescriptor(declaration, type.Parameters[i].GenericParameterConstraints[j]);
                        }
                    }

                }
                declaration.Write(">");
            }

            // Extends
            if (type.Extends.Count > 0)
            {
                declaration.Write(" extends ");
                for (var i = 0; i < type.Extends.Count; i++)
                {
                    if (i > 0)
                        declaration.Write(", ");
                    TypeDescriptor(declaration, type.Extends[i]);
                }
            }

            using (sb.OpenBrace(declaration.ToString()))
            {
                foreach (var property in type.Properties)
                {
                    if (property.JsonIgnore)
                        continue;

                    sb.Write(property.JsonPropertyName + ": ");
                    TypeDescriptor(sb, property.TypeDescriptor);
                    if (property.Nullable)
                        sb.Write(" | null");

                    sb.WriteLine(";");
                }
            }
            sb.WriteLine();
        }

        public static string TypeDescriptor(ApiTypeDescriptor typeDescriptor)
        {
            ScriptBuilder sb = new ScriptBuilder();
            TypeDescriptor(sb, typeDescriptor);
            return sb.ToString();
        }

        public static void TypeDescriptor(ScriptBuilder sb, ApiTypeDescriptor typeDescriptor)
        {
            foreach (var wrapper in typeDescriptor.Wrappers)
            {
                if (wrapper == ApiTypeWrapper.Enumerable)
                    sb.Write("Array<");
            }

            if (typeDescriptor.Type is ApiGenericParameterType)
                TypeDescriptor(sb, typeDescriptor.Type as ApiGenericParameterType, typeDescriptor);

            else if (typeDescriptor.Type is ApiPrimativeType)
                TypeDescriptor(sb, typeDescriptor.Type as ApiPrimativeType, typeDescriptor);

            else if (typeDescriptor.Type is ApiEnumType)
                TypeDescriptor(sb, typeDescriptor.Type as ApiEnumType, typeDescriptor);

            else if (typeDescriptor.Type is ApiComplexType)
                TypeDescriptor(sb, typeDescriptor.Type as ApiComplexType, typeDescriptor);

            else
                sb.Write("any");

            foreach (var wrapper in typeDescriptor.Wrappers)
            {
                if (wrapper == ApiTypeWrapper.Enumerable)
                    sb.Write(">");
            }
        }

        static void TypeDescriptor(ScriptBuilder sb, ApiGenericParameterType type, ApiTypeDescriptor typeDescriptor)
        {
            sb.Write(type.Name);

            if (typeDescriptor.Wrappers.Count == 1 && typeDescriptor.Wrappers[0] == ApiTypeWrapper.Nullable)
                sb.Write(" | null");
        }

        static void TypeDescriptor(ScriptBuilder sb, ApiPrimativeType type, ApiTypeDescriptor typeDescriptor)
        {
            var any = false;
            switch (type.TypeCode)
            {
                case ApiTypeCode.Boolean:
                    sb.Write("boolean");
                    break;

                case ApiTypeCode.String:
                case ApiTypeCode.Guid:
                case ApiTypeCode.DateTime:
                case ApiTypeCode.Date:
                    sb.Write("string");
                    break;

                case ApiTypeCode.Number:
                    sb.Write("number");
                    break;

                case ApiTypeCode.JObject:
                    sb.Write("{ [key: string]: any }");
                    break;

                default:
                    any = true;
                    sb.Write("any");
                    break;
            }

            if (!any && typeDescriptor.Wrappers.Count == 1 && typeDescriptor.Wrappers[0] == ApiTypeWrapper.Nullable)
                sb.Write(" | null");
        }

        static void TypeDescriptor(ScriptBuilder sb, ApiEnumType type, ApiTypeDescriptor typeDescriptor)
        {
            sb.Write(type.EnumStatic.Name);
            if (typeDescriptor.Wrappers.Count == 1 && typeDescriptor.Wrappers[0] == ApiTypeWrapper.Nullable)
               sb.Write(" | null");
        }

        static void TypeDescriptor(ScriptBuilder sb, ApiComplexType type, ApiTypeDescriptor typeDescriptor)
        {
            sb.Write(TypeName(type));

            if (typeDescriptor.GenericTypeArguments.Count > 0)
            {
                sb.Write("<");
                for (var i = 0; i < typeDescriptor.GenericTypeArguments.Count; i++)
                {
                    if (i > 0)
                        sb.Write(", ");
                    TypeDescriptor(sb, typeDescriptor.GenericTypeArguments[i]);
                }
                sb.Write(">");
            }
        }
    }
}
