using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Webfuel.Tools.Typefuel
{
    public class ApiSchema
    {
        public ApiSchema()
        {
            TypeContext = new ApiTypeContext(this);
        }

        public ApiTypeContext TypeContext { get; private set; }

        public List<ApiService> Services { get; } = new List<ApiService>();

        public Dictionary<Type, ApiEnum> Enums { get; } = new Dictionary<Type, ApiEnum>();

        public ApiEnum AnalyseEnum(Type enumType)
        {
            if (Enums.ContainsKey(enumType))
                return Enums[enumType];

            var typeInfo = enumType.GetTypeInfo();

            var apiEnum = new ApiEnum(this);

            apiEnum.Name = enumType.Name;
            apiEnum.IsEnum = typeInfo.IsEnum;

            // Keys
            foreach (var field in typeInfo.GetFields(BindingFlags.Public | BindingFlags.Static).Where(p => p.Name != "Values"))
            {
                apiEnum.Rows.Add(new ApiEnumRow(apiEnum)
                {
                    Name = field.Name.ToIdentifier(),
                    Value = field.IsLiteral ? field.GetRawConstantValue() : field.GetValue(null)
                });
            }

            if(apiEnum.Rows.Count > 0)
            {
                apiEnum.ValueType = TypeContext.GetTypeDescriptor(apiEnum.Rows[0].Value.GetType());
            }

            // Value Array
            var values = typeInfo.GetFields(BindingFlags.Public | BindingFlags.Static).FirstOrDefault(p => p.Name == "Values");
            var valueArray = values?.GetValue(null);
            if (valueArray != null && valueArray.GetType().IsArray)
            {
                apiEnum.ValueArray = valueArray as object[];
                // apiStatic.ValueType = TypeContext.GetTypeDescriptor(valueArray.GetType().GetElementType());
                //if (!(apiEnum.ValueType.Type is ApiComplexType))
                //    throw new InvalidOperationException("Static Values must be Complex Types");
            }

            return Enums[enumType] = apiEnum;
        }
    }
}
