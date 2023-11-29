using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Webfuel.Scribble
{
    static class Reflection
    {
        static Reflection()
        {
            // Register core assemblies for extension methods
            RegisterExtensionMethodAssembly(typeof(String).Assembly);
            RegisterExtensionMethodAssembly(typeof(System.Linq.Enumerable).Assembly);
            RegisterExtensionMethodAssembly(typeof(Reflection).Assembly);

            // Register core assembiles for constructable types
            RegisterConstructableAssembly(typeof(Reflection).Assembly);

            // Register primative constructable types
            RegisterConstructableType(typeof(String), "String", "string");
            RegisterConstructableType(typeof(Int32), "Int32", "int");
            RegisterConstructableType(typeof(Decimal), "Decimal", "decimal");
            RegisterConstructableType(typeof(float), "float");
            RegisterConstructableType(typeof(Boolean), "Boolean", "bool");
            RegisterConstructableType(typeof(List<>), "List");
            RegisterConstructableType(typeof(Dictionary<,>), "Dictionary");
        }

        // TypeInfo

        public static ScribbleTypeInfo GetTypeInfo(Type type)
        {

            return TypeInfoCache.GetOrAdd(type, key => new ScribbleTypeInfo(type));
        }

        static ConcurrentDictionary<Type, ScribbleTypeInfo> TypeInfoCache = new ConcurrentDictionary<Type, ScribbleTypeInfo>();

        // C# implicit conversion rules

        static ConcurrentDictionary<(Type, Type), bool> CanCastCache = new ConcurrentDictionary<(Type, Type), bool>();

        public static bool CanCast(Type from, Type to)
        {
            if (from == to)
                return true;

            if (from == Reflection.DynamicType || to == Reflection.DynamicType)
                return true;

            // Numeric Conversion Rules
            if (IsNumeric(from) && IsNumeric(to))
                return HasImplicitNumericConversion(from, to);

            // Type Cast with caching
            return CanCastCache.GetOrAdd((from, to), k => CanCastImpl(k.Item1, k.Item2));
        }

        static bool CanCastImpl(Type from, Type to)
        {
            // Inherits / Implements
            if (to.IsAssignableFrom(from))
                return true;

            // Implicit Conversion Operators
            if (HasImplicitConversionOperator(from, from, to) || HasImplicitConversionOperator(to, from, to))
                return true;

            // Handle Enum
            if (to.IsEnum)
                return CanCast(from, Enum.GetUnderlyingType(to));

            // Unwrap Nullables
            {
                var _to = Nullable.GetUnderlyingType(to);
                var _from = Nullable.GetUnderlyingType(from);

                if(_to != null || _from != null)
                    return CanCast(Nullable.GetUnderlyingType(from) ?? from, Nullable.GetUnderlyingType(to) ?? to);
            }

            return false;
        }

        public static bool HasImplicitNumericConversion(Type from, Type to)
        {
            TypeCode f = Type.GetTypeCode(from), t = Type.GetTypeCode(to);
            if (f == t) return true;
            switch (f)
            {
                case TypeCode.SByte: return t == TypeCode.Int16 || t == TypeCode.Int32 || t == TypeCode.Int64 || t == TypeCode.Single || t == TypeCode.Double || t == TypeCode.Decimal;
                case TypeCode.Byte: return t == TypeCode.Int16 || t == TypeCode.UInt16 || t == TypeCode.Int32 || t == TypeCode.UInt32 || t == TypeCode.Int64 || t == TypeCode.UInt64 || t == TypeCode.Single || t == TypeCode.Double || t == TypeCode.Decimal;
                case TypeCode.Int16: return t == TypeCode.Int32 || t == TypeCode.Int64 || t == TypeCode.Single || t == TypeCode.Double || t == TypeCode.Decimal;
                case TypeCode.UInt16: return t == TypeCode.Int32 || t == TypeCode.UInt32 || t == TypeCode.Int64 || t == TypeCode.UInt64 || t == TypeCode.Single || t == TypeCode.Double || t == TypeCode.Decimal;
                case TypeCode.Int32: return t == TypeCode.Int64 || t == TypeCode.Single || t == TypeCode.Double || t == TypeCode.Decimal;
                case TypeCode.UInt32: return t == TypeCode.Int64 || t == TypeCode.UInt64 || t == TypeCode.Single || t == TypeCode.Double || t == TypeCode.Decimal;
                case TypeCode.Int64: return t == TypeCode.Single || t == TypeCode.Double || t == TypeCode.Decimal;
                case TypeCode.UInt64: return t == TypeCode.Single || t == TypeCode.Double || t == TypeCode.Decimal;
                case TypeCode.Char: return t == TypeCode.UInt16 || t == TypeCode.Int32 || t == TypeCode.UInt32 || t == TypeCode.Int64 || t == TypeCode.UInt64 || t == TypeCode.Single || t == TypeCode.Double || t == TypeCode.Decimal;
                case TypeCode.Single: return t == TypeCode.Double;
            }
            return false;
        }

        public static bool HasImplicitConversionOperator(Type definedOn, Type from, Type to)
        {
            return definedOn.GetMethods(BindingFlags.Public | BindingFlags.Static)
                 .Where(mi => mi.Name == "op_Implicit" && mi.ReturnType == to)
                 .Any(mi => { ParameterInfo? pi = mi.GetParameters().FirstOrDefault(); return pi != null && pi.ParameterType == from; });
        }

        // Constructable Types

        public static void RegisterConstructableAssembly(Assembly assembly)
        {
            if (ConstructableAssemblies.Contains(assembly))
                return; // We have already analysed this assembly
            ConstructableAssemblies.Add(assembly);

            // Find all constructable types

            foreach (var type in assembly.GetTypes())
            {
                var constructableAttribute = type.GetCustomAttribute<ScribbleConstructableAttribute>();
                if (constructableAttribute == null)
                    continue;

                RegisterConstructableType(type, FixConstructableTypeName(type.Name));
            }
        }

        public static void RegisterConstructableType(Type type, string name, params string[] aliases)
        {
            var typeInfo = GetTypeInfo(type);

            if (typeInfo.IsGenericTypeDefinition)
                ConstructableTypeDefinitions.Add(name, typeInfo);
            else
                ConstructableTypes.Add(ScribbleTypeName.Create(type.Name), typeInfo);

            foreach (var alias in aliases)
                ConstructableTypeAliases.Add(alias, name);
        }

        public static ScribbleTypeInfo? GetConstructableType(ScribbleTypeName typeName)
        {
            if (ConstructableTypes.ContainsKey(typeName))
                return ConstructableTypes[typeName];

            if (!ConstructableTypeDefinitions.ContainsKey(typeName.Name))
                return null; // Not constructable from any type definition

            var constructableTypeDefinition = ConstructableTypeDefinitions[typeName.Name];
            if (constructableTypeDefinition.GenericTypeParameterCount != typeName.Arguments.Count)
                return null; // Type parameter counts don't match

            var genericType = constructableTypeDefinition.MakeGenericType(typeName.Arguments);

            // Cache this for next time
            ConstructableTypes.Add(typeName, genericType);
            return genericType;
        }

        public static IEnumerable<Type> MapConstructableTypes(IEnumerable<ScribbleTypeName>? typeNames)
        {
            if (typeNames == null)
                yield break;

            foreach (var typeName in typeNames)
                yield return GetConstructableType(typeName)?.Type ?? throw new InvalidOperationException($"'{typeName}' is not constructable");
        }


        static Dictionary<string, ScribbleTypeInfo> ConstructableTypeDefinitions = new Dictionary<string, ScribbleTypeInfo>();

        static Dictionary<ScribbleTypeName, ScribbleTypeInfo> ConstructableTypes = new Dictionary<ScribbleTypeName, ScribbleTypeInfo>();

        static Dictionary<string, string> ConstructableTypeAliases = new Dictionary<string, string>();

        public static string FixConstructableTypeName(string name)
        {
            if (name.Contains('`'))
                name = name.Split('`')[0];

            if (ConstructableTypeAliases.ContainsKey(name))
                name = ConstructableTypeAliases[name];

            return name;
        }

        static List<Assembly> ConstructableAssemblies = new List<Assembly>();

        // Extension Methods

        public static void RegisterExtensionMethodAssembly(Assembly assembly)
        {
            if (ExtensionMethodAssemblies.Contains(assembly))
                return; // We have already analysed this assembly
            ExtensionMethodAssemblies.Add(assembly);

            // Find all candidate extension methods (irrespective of target type)

            foreach (var type in assembly.GetTypes())
            {
                if (!type.IsAbstract || !type.IsSealed || type.IsGenericType || type.IsNested)
                    continue;

                foreach (var method in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
                {
                    if (!method.IsDefined(typeof(System.Runtime.CompilerServices.ExtensionAttribute), false))
                        continue;

                    var parameters = method.GetParameters();
                    if (parameters.Length == 0)
                        continue;

                    ExtensionMethods.Add(method);
                }
            }
        }

        static List<MethodInfo> ExtensionMethods = new List<MethodInfo>();

        static List<Assembly> ExtensionMethodAssemblies = new List<Assembly>();

        public static List<MethodInfo> GetExtensionMethods(Type targetType, string name)
        {
            var result = new List<MethodInfo>();

            foreach (var method in ExtensionMethods)
            {
                if (method.Name != name)
                    continue;

                var extensionType = method.GetParameters()[0].ParameterType;

                // Try a simple cast
                if (Reflection.CanCast(targetType, extensionType)) 
                { 
                    result.Add(method);
                    continue;
                }

                if(GetCommonGenericInterface(extensionType, targetType) != null)
                {
                    result.Add(method);
                    continue;
                }

            }
            return result;
        }

        public static Type? GetCommonGenericInterface(Type to, Type from)
        {
            var extensionTypeDefinition = to.IsConstructedGenericType ? to.GetGenericTypeDefinition() : UnknownType;

            if (extensionTypeDefinition != UnknownType && from.IsConstructedGenericType && extensionTypeDefinition == from.GetGenericTypeDefinition())
                return extensionTypeDefinition;

            foreach (var _interface in from.GetInterfaces())
            {
                if (to.IsAssignableFrom(_interface))
                    return to;

                if (extensionTypeDefinition != UnknownType && _interface.IsConstructedGenericType && extensionTypeDefinition == _interface.GetGenericTypeDefinition())
                    return extensionTypeDefinition;
            }

            return null;
        }

        // Helpers

        public static Type? GetEnumerableElementType(Type type)
        {
            // type is Array
            if (type.IsArray)
                return type.GetElementType();

            // type is IEnumerable<T>;
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                return type.GetGenericArguments()[0];

            // type implements IEnumerable<T>;
            var elementType = type.GetInterfaces()
                                    .Where(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                                    .Select(t => t.GenericTypeArguments[0])
                                    .FirstOrDefault();

            if (elementType != null)
                return elementType;

            return null;
        }

        public static bool IsBoolean(Type type)
        {
            return Type.GetTypeCode(type) == TypeCode.Boolean;
        }

        public static bool IsNumeric(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Char:
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsString(Type type)
        {
            return Type.GetTypeCode(type) == TypeCode.String;
        }

        public static bool IsNullable(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static Type NullableUnderlyingType(Type type)
        {
            return Nullable.GetUnderlyingType(type) ?? type;
        }

        // Standard Type Tokens

        public static Type UnknownType { get; } = typeof(TypeUnknown);
        class TypeUnknown { }

        public static Type NullType { get; } = typeof(TypeNull);
        class TypeNull { }

        public static Type DefaultType { get; } = typeof(TypeDefault);
        class TypeDefault { }

        public static Type DynamicType { get; } = typeof(TypeDynamic);
        class TypeDynamic { };
    }
}
