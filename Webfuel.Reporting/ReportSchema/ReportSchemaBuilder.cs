using Microsoft.VisualBasic.FileIO;
using System.Linq.Expressions;
using System.Reflection;

namespace Webfuel.Reporting
{
    public class ReportSchemaBuilder<TContext> where TContext : class
    {
        public ReportSchemaBuilder(Guid reportProviderId)
        {
            Schema = new ReportSchema { ReportProviderId = reportProviderId };
        }

        public ReportSchema Schema { get; }

        public void AddField(ReportField field)
        {
            Schema.AddField(field);
        }

        public void AddProperty<TField>(
            Guid id,
            Expression<Func<TContext, TField>> expr,
            string? name = null,
            ReportFieldType? fieldType = null)
        {
            Schema.AddField(new ReportPropertyField
            {
                Id = id,
                Name = name ?? GetExprName(expr),
                Accessor = o => expr.Compile()((TContext)o),
                FieldType = fieldType ?? GetExprFieldType(expr),
            });
        }

        public void AddMethod<TField>(
            Guid id,
            Expression<Func<TContext, Task<TField>>> expr,
            string? name = null,
            ReportFieldType? fieldType = null)
        {
            Schema.AddField(new ReportMethodField
            {
                Id = id,
                Name = name ?? GetExprName(expr),
                Accessor = async o => await expr.Compile()((TContext)o),
                FieldType = fieldType ?? GetExprFieldType(expr),
            });
        }

        public void AddReference<TReferenceProvider>(
            Guid id,
            Expression<Func<TContext, Guid?>> expr,
            string? name = null) where TReferenceProvider : IReportReferenceProvider
        {
            Schema.AddField(new ReportReferenceField
            {
                Id = id,
                Name = name ?? GetExprName(expr),
                Accessor = o => expr.Compile()((TContext)o),
                ReferenceProviderType = typeof(TReferenceProvider),
                FieldType = ReportFieldType.Reference,
            });
        }

        public void AddReference<TReferenceProvider>(
            Guid id,
            Expression<Func<TContext, Guid>> expr,
            string? name = null) where TReferenceProvider : IReportReferenceProvider
        {
            Schema.AddField(new ReportReferenceField
            {
                Id = id,
                Name = name ?? GetExprName(expr),
                Accessor = o => expr.Compile()((TContext)o),
                ReferenceProviderType = typeof(TReferenceProvider),
                FieldType = ReportFieldType.Reference,
            });
        }

        public void AddReferenceList<TReferenceProvider>(
            Guid id,
            Expression<Func<TContext, IEnumerable<Guid>>> expr,
            string? name = null) where TReferenceProvider : IReportReferenceProvider
        {
            Schema.AddField(new ReportReferenceListField
            {
                Id = id,
                Name = name ?? GetExprName(expr),
                Accessor = o => expr.Compile()((TContext)o),
                ReferenceProviderType = typeof(TReferenceProvider),
                FieldType = ReportFieldType.ReferenceList,
            });
        }

        public void AddExpression(
            Guid id,
            string name,
            string expression)
        {
            Schema.AddField(new ReportExpressionField
            {
                Id = id,
                Name = name,
                Expression = expression,
                FieldType = ReportFieldType.Expression,
            });
        }

        // Helpers

        string GetExprName<TProperty>(Expression<Func<TContext, TProperty>> accessor)
        {
            if (accessor.Body is MethodCallExpression methodCall)
                return FormatName(methodCall.Method.Name);

            if (accessor.Body is MemberExpression member)
                return FormatName(member.Member.Name);

            throw new ArgumentException(string.Format(
                "Expression '{0}' does not refer to a field, method or property.",
                accessor.ToString()));
        }

        ReportFieldType GetExprFieldType<TProperty>(Expression<Func<TContext, TProperty>> accessor)
        {
            if (accessor.Body is MethodCallExpression methodCall)
                return MapFieldType(methodCall.Method.ReturnType);

            if (accessor.Body is MemberExpression member)
            {
                if (member.Member is FieldInfo fieldInfo)
                    return MapFieldType(fieldInfo.FieldType);

                if (member.Member is PropertyInfo propertyInfo)
                    return MapFieldType(propertyInfo.PropertyType);
            }

            throw new ArgumentException(string.Format(
                "Expression '{0}' does not refer to a field, method or property.",
                accessor.ToString()));
        }

        static string FormatName(string input)
        {
            input = SplitCamelCase(input);

            if (input.EndsWith(" Id"))
                input = input.Substring(0, input.Length - 3);

            if (input.EndsWith(" Ids"))
                input = input.Substring(0, input.Length - 4);

            return input;
        }

        static ReportFieldType MapFieldType(Type clrType)
        {
            var type = UnwrapBaseType(clrType);

            if (IsNumericType(type))
                return ReportFieldType.Number;

            if (type == typeof(DateOnly))
                return ReportFieldType.Date;

            if (type == typeof(DateTimeOffset))
                return ReportFieldType.DateTime;

            if (type == typeof(bool))
                return ReportFieldType.Boolean;

            return ReportFieldType.String;
        }

        static Type UnwrapBaseType(Type type)
        {
            if (IsTaskType(type))
                type = GetGenericBaseType(type, 0);

            if (IsNullableType(type))
                type = GetGenericBaseType(type, 0);

            return type;
        }

        static Type GetGenericBaseType(Type type, int index)
        {
            return type.GetGenericArguments()[index];
        }

        static bool IsNullableType(Type type)
        {
            if (type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                return true;
            return false;
        }

        static bool IsTaskType(Type type)
        {
            return type.GetTypeInfo().IsGenericType && (type.GetGenericTypeDefinition() == typeof(Task<>));
        }

        static bool IsNumericType(Type type)
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

        static string SplitCamelCase(string input)
        {
            return System.Text.RegularExpressions.Regex.Replace(input, "([A-Z])", " $1", System.Text.RegularExpressions.RegexOptions.Compiled).Trim();
        }
    }
}
