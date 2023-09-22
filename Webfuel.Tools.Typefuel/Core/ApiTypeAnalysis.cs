using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Webfuel.Tools.Typefuel
{
    public enum ApiTypeCode
    {
        Unknown = 0,

        Number,
        String,
        Guid,
        DateTime,
        Date,
        Boolean,
        Void,
        JObject
    }

    public enum ApiTypeWrapper
    {
        Task,
        Enumerable,
        Nullable,
    }

    public static class ApiTypeAnalysis
    {
        public static bool IsApiType(Type type)
        {
            if (type == null)
                return false;

            if (type.IsConstructedGenericType)
                type = type.GetGenericTypeDefinition();

            if (!type.FullName.StartsWith("Webfuel."))
                return false;

            if (type.GetCustomAttribute<ApiIgnoreAttribute>() != null)
                return false;

            return true;
        }

        public static Type UnwrapBaseType(Type type, List<ApiTypeWrapper> wrappers)
        {
            if (type == typeof(Task))
            {
                wrappers.Add(ApiTypeWrapper.Task);
                return typeof(void);
            }

            while (true)
            {
                if (IsNullableType(type))
                    wrappers.Add(ApiTypeWrapper.Nullable);
                else if (IsEnumerableType(type))
                    wrappers.Add(ApiTypeWrapper.Enumerable);
                else if (IsTaskType(type))
                    wrappers.Add(ApiTypeWrapper.Task);
                else
                    break;

                type = ApiTypeAnalysis.GetGenericBaseType(type, index: 0);
            }
            return type;
        }

        public static Type GetGenericBaseType(Type type, int index)
        {
            if (!IsGenericType(type))
                return type;
            return type.GetGenericArguments()[index];
        }

        public static ApiTypeCode GetPrimativeTypeCode(Type type)
        {
            if (IsNumericType(type))
                return ApiTypeCode.Number;
            if (IsStringType(type))
                return ApiTypeCode.String;
            if (IsGuidType(type))
                return ApiTypeCode.Guid;
            if (IsDateTimeType(type))
                return ApiTypeCode.DateTime;
            if (IsDateType(type))
                return ApiTypeCode.Date;
            if (IsBooleanType(type))
                return ApiTypeCode.Boolean;
            if (IsVoidType(type))
                return ApiTypeCode.Void;
            //if (IsJObjectType(type))
            //    return ApiTypeCode.JObject;

            return ApiTypeCode.Unknown; // Not a primative type
        }

        // Is Methods

        public static bool IsGenericType(Type type)
        {
            return type.GetTypeInfo().IsGenericType;
        }

        public static bool IsPrimativeType(Type type)
        {
            return GetPrimativeTypeCode(type) != ApiTypeCode.Unknown;
        }

        public static bool IsTaskType(Type type)
        {
            return type.GetTypeInfo().IsGenericType && (type.GetGenericTypeDefinition() == typeof(Task<>));
        }

        public static bool IsEnumerableType(Type type)
        {
            return type.GetTypeInfo().IsGenericType && (type.GetGenericTypeDefinition() == typeof(IEnumerable<>) || type.GetGenericTypeDefinition() == typeof(List<>) || type.GetGenericTypeDefinition() == typeof(IList<>));
        }

        public static bool IsNullableType(Type type)
        {
            if (type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                return true;
            return false;
        }

        public static bool IsValueType(Type type)
        {
            return type.GetTypeInfo().IsValueType;
        }

        public static bool IsNumericType(Type type)
        {
            return
                type == typeof(Byte) ||
                type == typeof(SByte) ||
                type == typeof(UInt16) ||
                type == typeof(UInt32) ||
                type == typeof(UInt64) ||
                type == typeof(Int16) ||
                type == typeof(Int32) ||
                type == typeof(Int64) ||
                type == typeof(Decimal) ||
                type == typeof(Double) ||
                type == typeof(Single);
        }

        public static bool IsStringType(Type type)
        {
            if (type == typeof(string))
                return true;
            return false;
        }

        public static bool IsBooleanType(Type type)
        {
            if (type == typeof(Boolean))
                return true;
            return false;
        }

        public static bool IsGuidType(Type type)
        {
            if (type == typeof(Guid))
                return true;
            return false;
        }

        public static bool IsDateTimeType(Type type)
        {
            if (type == typeof(DateTimeOffset))
                return true;
            return false;
        }

        public static bool IsDateType(Type type)
        {
            if (type == typeof(DateOnly))
                return true;
            return false;
        }

        public static bool IsVoidType(Type type)
        {
            if (type == typeof(void))
                return true;
            return false;
        }

        public static bool IsEnumType(Type type)
        {
            return type.IsEnum;
        }
    }
}
