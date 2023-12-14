using DocumentFormat.OpenXml.InkML;
using Irony.Ast;
using Microsoft.VisualBasic.FileIO;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Webfuel.Reporting
{
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
            string name,
            Func<TContext, TField> accessor)
        {
            Schema.AddField(new ReportPropertyField
            {
                Id = id,
                Name = name,
                Accessor = o => accessor((TContext)o),
                Mapping = Mapping,
                FieldType = MapFieldType(typeof(TField)),
            });
            return this;
        }

        // Add Async Expression
        public ReportSchemaBuilder<TContext> Add<TField>(
            Guid id,
            string name,
            Func<TContext, Task<TField>> accessor)
        {
            Schema.AddField(new ReportMethodField
            {
                Id = id,
                Name = name,
                Accessor = async o => await accessor((TContext)o),
                Mapping = Mapping,
                FieldType = MapFieldType(typeof(TField)),
            });
            return this;
        }

        /////////////////////////////////////////////////////////////////////////////
        // References 

        public ReportSchemaBuilder<TContext> Ref(
            Guid id,
            string name)
        {
            if (Mapping == null)
                throw new ArgumentException("Mapping is null");

            Schema.AddField(new ReportReferenceField
            {
                Id = id,
                Name = name,
                Mapping = Mapping,
                FieldType = ReportFieldType.Reference,
            });
            return this;
        }

        /////////////////////////////////////////////////////////////////////////////
        // Mapping

        public ReportSchemaBuilder<TEntity> Map<TEntity>(
            Func<TContext, Guid?> accessor) where TEntity : class
        {
            var mapping = new ReportMapping<TEntity>
            {
                Accessor = o => accessor((TContext)o),
                ParentMapping = Mapping,
            };
            return new ReportSchemaBuilder<TEntity>(Schema, mapping);
        }

        public ReportSchemaBuilder<TEntity> Map<TEntity>(
            Func<TContext, Guid> accessor) where TEntity : class
        {
            var mapping = new ReportMapping<TEntity>
            {
                Accessor = o => accessor((TContext)o),
                ParentMapping = Mapping,
            };
            return new ReportSchemaBuilder<TEntity>(Schema, mapping);
        }

        /////////////////////////////////////////////////////////////////////////////
        // Helpers

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
