using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Tools.Typefuel
{
    public class ApiTypeContext
    {
        public readonly ApiSchema Schema;

        private readonly ApiPrimativeType UnknownType;

        public ApiTypeContext(ApiSchema schema)
        {
            Schema = schema;
            UnknownType = new ApiPrimativeType(this, ApiTypeCode.Unknown);
        }

        public Dictionary<Type, ApiType> TypeMapping { get; } = new Dictionary<Type, ApiType>();

        public ApiTypeDescriptor GetTypeDescriptor(Type clrType)
        {
            var typeDescriptor = new ApiTypeDescriptor();
            var baseType = ApiTypeAnalysis.UnwrapBaseType(clrType, typeDescriptor.Wrappers);

            typeDescriptor.Type = MapType(baseType);

            // If this type is generic then store the type arguments
            if (ApiTypeAnalysis.IsGenericType(baseType))
            {
                foreach (var argument in baseType.GenericTypeArguments)
                {
                    typeDescriptor.GenericTypeArguments.Add(GetTypeDescriptor(argument));
                }
            }

            return typeDescriptor;
        }

        // This is where we create all types
        ApiType MapType(Type clrType)
        {
            if (TypeMapping.ContainsKey(clrType))
                return TypeMapping[clrType];

            // Generic types are easy
            if (clrType.IsGenericParameter)
                return new ApiGenericParameterType(this, clrType, Schema);

            // Primative types are easy
            if (ApiTypeAnalysis.IsPrimativeType(clrType))
                return TypeMapping[clrType] = new ApiPrimativeType(this, ApiTypeAnalysis.GetPrimativeTypeCode(clrType));

            // Enums are mapped against their matching static type
            if (ApiTypeAnalysis.IsEnumType(clrType))
                return TypeMapping[clrType] = new ApiEnumType(this, Schema.AnalyseStatic(clrType));

            // We only analyse certain types (i.e. not system/microsoft types)
            if (!ApiTypeAnalysis.IsApiType(clrType))
                return UnknownType;

            ApiComplexType complexType = null;

            // We register generic types agains their generic type definitions
            if (ApiTypeAnalysis.IsGenericType(clrType))
            {
                clrType = clrType.GetGenericTypeDefinition();
                if (TypeMapping.ContainsKey(clrType))
                    return TypeMapping[clrType];
            }

            var signature = String.Empty;
            var typefuelAttribute = clrType.GetCustomAttribute<TypefuelInterfaceAttribute>();
            if (typefuelAttribute != null)
                signature = typefuelAttribute.Signature;

            complexType = new ApiComplexType(this, clrType.Name.Split(new char[] { '`' })[0], signature);

            // Extract the generic type parameters
            foreach (var genericTypeParameter in clrType.GetTypeInfo().GenericTypeParameters)
            {
                complexType.Parameters.Add(new ApiGenericParameterType(this, genericTypeParameter, Schema));
            }

            // Store the complex type before we iterate otherwise we can end up with an infinite recursion
            TypeMapping[clrType] = complexType;

            // Iterate over the properties of the type and map them
            foreach (var property in clrType.GetTypeInfo().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var apiTypeProperty = new ApiTypeProperty
                {
                    Name = property.Name,
                    TypeDescriptor = GetTypeDescriptor(property.PropertyType)
                };

                apiTypeProperty.JsonIgnore = false;
                apiTypeProperty.JsonPropertyName = apiTypeProperty.Name.ToCamelCase();

                /*
                apiTypeProperty.JsonIgnore = property.GetCustomAttribute<JsonIgnoreAttribute>() != null;

                var jsonProperty = property.GetCustomAttribute<JsonPropertyAttribute>();
                if (jsonProperty != null && !String.IsNullOrEmpty(jsonProperty.PropertyName))
                    apiTypeProperty.JsonPropertyName = jsonProperty.PropertyName;
                else
                    apiTypeProperty.JsonPropertyName = apiTypeProperty.Name.ToCamelCase();
                */

                var nullableAttribute = property.GetCustomAttributes().FirstOrDefault(p => p.GetType().Name == "NullableAttribute");

                if (ApiTypeAnalysis.IsNullableType(property.PropertyType))
                    apiTypeProperty.Nullable = true;

                else if (nullableAttribute != null)
                {
                    var nullableFlags = nullableAttribute.GetType().GetField("NullableFlags").GetValue(nullableAttribute) as byte[];

                    //if (nullableFlags.Length != 1)
                    //    throw new InvalidOperationException("Don't know how to interpret multiple NullableFlags");

                    apiTypeProperty.Nullable = nullableFlags[0] == 2;
                }

                else
                    apiTypeProperty.Nullable = false;

                complexType.Properties.Add(apiTypeProperty);
            }

            // Add base type
            if (ApiTypeAnalysis.IsApiType(clrType.GetTypeInfo().BaseType))
            {
                complexType.Extends.Add(GetTypeDescriptor(clrType.GetTypeInfo().BaseType));
            }

            // Add interfaces
            foreach (var extends in clrType.GetInterfaces())
            {
                if (ApiTypeAnalysis.IsApiType(extends))
                    complexType.Extends.Add(GetTypeDescriptor(extends));
            }

            if (clrType.GetTypeInfo().IsInterface)
                complexType.IsInterface = true;

            return complexType;
        }
    }

    public class ApiTypeDescriptor
    {
        public ApiType Type { get; set; }

        // We strip out standard wrappers (Nullable, Task, IEnumerable)
        public List<ApiTypeWrapper> Wrappers { get; } = new List<ApiTypeWrapper>();

        // Arguments to use if Type is a generic type
        public List<ApiTypeDescriptor> GenericTypeArguments { get; } = new List<ApiTypeDescriptor>();

        // Constraints if this is a generic type parameter
        public List<ApiTypeDescriptor> GenericParameterConstraints { get; } = new List<ApiTypeDescriptor>();

        public IEnumerable<ApiType> EnumerateTypes()
        {
            yield return Type;

            foreach (var argument in GenericTypeArguments)
            {
                foreach(var type in argument.EnumerateTypes())
                {
                    yield return type;
                }
            }
        }
    }

    public class ApiTypeProperty
    {
        public string Name { get; set; }

        public string JsonPropertyName { get; set; }

        public bool JsonIgnore { get; set; }

        public bool Nullable { get; set; }

        public ApiTypeDescriptor TypeDescriptor { get; set; }
    }

    public abstract class ApiType
    {
        public readonly ApiTypeContext Context;

        protected ApiType(ApiTypeContext context)
        {
            Context = context;
        }

        public bool IsInterface { get; set; }
    }

    public class ApiPrimativeType : ApiType
    {
        public ApiPrimativeType(ApiTypeContext context, ApiTypeCode typeCode)
            : base(context)
        {
            TypeCode = typeCode;
        }

        public ApiTypeCode TypeCode { get; } = ApiTypeCode.Unknown;
    }

    public class ApiEnumType: ApiType
    {
        public ApiEnumType(ApiTypeContext context, ApiStatic enumStatic)
            : base(context)
        {
            EnumStatic = enumStatic;
        }

        public ApiStatic EnumStatic { get; } 
    }

    public class ApiComplexType : ApiType
    {
        public ApiComplexType(ApiTypeContext context, string name, string signature)
            : base(context)
        {
            Name = name;
            Signature = signature;
        }

        public string Name { get; private set; }

        public List<ApiTypeProperty> Properties { get; } = new List<ApiTypeProperty>();

        public List<ApiGenericParameterType> Parameters { get; } = new List<ApiGenericParameterType>();

        public List<ApiTypeDescriptor> Extends { get; } = new List<ApiTypeDescriptor>();

        public string Signature { get; set; } = String.Empty;
    }

    public class ApiGenericParameterType : ApiType
    {
        public ApiGenericParameterType(ApiTypeContext context, Type clrType, ApiSchema schema)
            : base(context)
        {
            Name = clrType.Name;

            // If this is a generic parameter exract any constraints
            if (clrType.IsGenericParameter)
            {
                foreach (var constraint in clrType.GetGenericParameterConstraints())
                {
                    GenericParameterConstraints.Add(schema.TypeContext.GetTypeDescriptor(constraint));
                }
            }

        }

        public string Name { get; private set; }

        public List<ApiTypeDescriptor> GenericParameterConstraints { get; } = new List<ApiTypeDescriptor>();
    }
}
