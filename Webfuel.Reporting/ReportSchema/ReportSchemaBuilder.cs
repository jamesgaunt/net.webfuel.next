using DocumentFormat.OpenXml.InkML;
using Irony.Ast;
using Microsoft.VisualBasic.FileIO;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Webfuel.Reporting
{

    public interface IReportSchemaBuilderAdd<TContext> where TContext : class
    {
        ReportSchemaBuilder<TContext> Add<TField>(
            Guid id,
            Expression<Func<TContext, TField>> expr,
            string? name = null,
            ReportFieldType? fieldType = null);

        ReportSchemaBuilder<TContext> Add<TField>(
            Guid id,
            Expression<Func<TContext, Task<TField>>> expr,
            string? name = null,
            ReportFieldType? fieldType = null);
    }

    public interface IReportSchemaBuilerMap<TContext> where TContext : class
    {
        ReportSchemaBuilder<TEntity> Map<TEntity>(Expression<Func<TContext, Guid?>> expr) where TEntity : class;

        ReportSchemaBuilder<TEntity> Map<TEntity>(Expression<Func<TContext, Guid>> expr) where TEntity : class;
    }
    public interface IReportSchemaBuilerRef<TContext> where TContext : class
    {
        ReportSchemaBuilder<TContext> Ref(
            Guid id,
            string? name = null);
    }

    public class ReportSchemaBuilder<TContext> where TContext : class
    {
        internal ReportSchemaBuilder(ReportSchema schema, ReportMapping<TContext> mapping)
        {
            Schema = schema;
            Mapping = mapping;
        }

        ReportSchemaBuilder(Guid reportProviderId)
        {
            Schema = new ReportSchema
            {
                ReportProviderId = reportProviderId
            };
        }

        public static ReportSchemaBuilder<TContext> Create(Guid reportProviderId) 
        {
            return new ReportSchemaBuilder<TContext>(reportProviderId);
        }

        public ReportSchema Schema { get; }

        internal ReportMapping<TContext>? Mapping { get; set; }

        /////////////////////////////////////////////////////////////////////////////
        // Add Expressions

        // Add Property Expression
        public ReportSchemaBuilder<TContext> Add<TField>(
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
                Mapping = Mapping,
                FieldType = fieldType ?? GetExprFieldType(expr),
            });
            return this;
        }

        // Add Async Expression
        public ReportSchemaBuilder<TContext> Add<TField>(
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
                Mapping = Mapping,
                FieldType = fieldType ?? GetExprFieldType(expr),
            });
            return this;
        }

        /////////////////////////////////////////////////////////////////////////////
        // References 

        public ReportSchemaBuilder<TContext> Ref(
            Guid id,
            string? name = null)
        {
            if (Mapping == null)
                throw new ArgumentException("Mapping is null");

            Schema.AddField(new ReportReferenceField
            {
                Id = id,
                Name = name ?? Mapping.Name,
                Mapping = Mapping,
                FieldType = ReportFieldType.Reference,
            });
            return this;
        }

        /////////////////////////////////////////////////////////////////////////////
        // Mapping

        public ReportSchemaBuilder<TEntity> Map<TEntity>(Expression<Func<TContext, Guid?>> expr) where TEntity : class
        {
            var mapping = new ReportMapping<TEntity>
            {
                Accessor = o => expr.Compile()((TContext)o),
                ParentMapping = Mapping,
                Name = GetExprName(expr)
            };
            return new ReportSchemaBuilder<TEntity>(Schema, mapping);
        }

        public ReportSchemaBuilder<TEntity> Map<TEntity>(Expression<Func<TContext, Guid>> expr) where TEntity : class
        {
            var mapping = new ReportMapping<TEntity>
            {
                Accessor = o => expr.Compile()((TContext)o),
                ParentMapping = Mapping,
                Name = GetExprName(expr)
            };
            return new ReportSchemaBuilder<TEntity>(Schema, mapping);
        }

        /////////////////////////////////////////////////////////////////////////////
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

        string FormatName(string input)
        {
            input = SplitCamelCase(input);

            if (input.EndsWith(" Id"))
                input = input.Substring(0, input.Length - 3);

            if (input.EndsWith(" Ids"))
                input = input.Substring(0, input.Length - 4);

            if (Mapping != null)
                input = Mapping.Name + " " + input;

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
            var result = new StringBuilder();

            for(var i = 0; i < input.Length; i++)
            {
                if(i == 0)
                {
                    result.Append(input[i]);
                    continue;
                }

                if (char.IsUpper(input[i]) && char.IsLower(input[i - 1]))
                    result.Append(" ");

                else if (char.IsUpper(input[i]) && i < input.Length - 1 && char.IsLower(input[i + 1]))
                    result.Append(" ");

                result.Append(input[i]);
            }

            return result.ToString();
        }
    }
}
