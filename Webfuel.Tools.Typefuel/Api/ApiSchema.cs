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

        public List<ApiController> Controllers { get; } = new List<ApiController>();

        public Dictionary<Type, ApiStatic> Static { get; } = new Dictionary<Type, ApiStatic>();


        public ApiStatic AnalyseStatic(Type staticType)
        {
            if (Static.ContainsKey(staticType))
                return Static[staticType];

            var typeInfo = staticType.GetTypeInfo();

            var apiStatic = new ApiStatic(this);

            apiStatic.Name = staticType.Name;
            apiStatic.IsEnum = typeInfo.IsEnum;

            // Keys
            foreach (var field in typeInfo.GetFields(BindingFlags.Public | BindingFlags.Static).Where(p => p.Name != "Values"))
            {
                apiStatic.Rows.Add(new ApiStaticRow(apiStatic)
                {
                    Name = field.Name.ToIdentifier(),
                    Value = field.IsLiteral ? field.GetRawConstantValue() : field.GetValue(null)
                });
            }

            // Value Array
            var values = typeInfo.GetFields(BindingFlags.Public | BindingFlags.Static).FirstOrDefault(p => p.Name == "Values");
            var valueArray = values?.GetValue(null);
            if (valueArray != null && valueArray.GetType().IsArray)
            {
                apiStatic.ValueArray = valueArray as object[];
                apiStatic.ValueType = TypeContext.GetTypeDescriptor(valueArray.GetType().GetElementType());
                if (!(apiStatic.ValueType.Type is ApiComplexType))
                    throw new InvalidOperationException("Static Values must be Complex Types");
            }

            return Static[staticType] = apiStatic;
        }
    }
}
