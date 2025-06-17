using System.Reflection;
using System.Text;

namespace Webfuel.Reporting;

public class ReportSchemaBuilder<TContext> where TContext : class
{
    internal ReportSchemaBuilder(ReportSchema schema, IReportMapping mapping)
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

    internal IReportMapping? Mapping { get; set; }

    /////////////////////////////////////////////////////////////////////////////
    // Add Expressions

    // Add Property Expression
    public void Add<TField>(
        Guid id,
        string name,
        Func<TContext, TField> accessor,
        bool filterable = true)
    {
        Schema.AddField(new ReportField
        {
            Id = id,
            Name = name,
            Accessor = new ReportPropertyAccessor { Accessor = o => accessor((TContext)o) },
            Mapping = Mapping,
            MultiValued = Mapping?.MultiValued ?? false,
            Filterable = filterable,
            FieldType = MapFieldType(typeof(TField)),
        });
    }

    // Add Async Expression
    public void Add<TField>(
        Guid id,
        string name,
        Func<TContext, IServiceProvider, Task<TField>> accessor)
    {
        Schema.AddField(new ReportField
        {
            Id = id,
            Name = name,
            Accessor = new ReportAsyncAccessor { Accessor = async (o, s) => await accessor((TContext)o, s) },
            Mapping = Mapping,
            MultiValued = Mapping?.MultiValued ?? false,
            Filterable = true,
            FieldType = MapFieldType(typeof(TField)),
        });
    }

    // Add Custom Field
    public void Add(ReportField field)
    {
        Schema.AddField(field);
    }

    /////////////////////////////////////////////////////////////////////////////
    // Mapping

    public void Map<TEntity>(
        Guid id,
        string name,
        Func<TContext, Guid?> accessor,
        Action<ReportSchemaBuilder<TEntity>>? action = null)
        where TEntity : class
    {
        var mapping = new ReportMapping<TEntity, IReportMap<TEntity>>
        {
            Accessor = o => accessor((TContext)o),
            ParentMapping = Mapping,
        };

        MapImpl<TEntity, IReportMap<TEntity>>(id: id, name: name, mapping: mapping, action: action);
    }

    public void Map<TEntity>(
        Guid id,
        string name,
        Func<TContext, IServiceProvider, IReportMap<TEntity>, Task<Guid?>> accessor,
        Action<ReportSchemaBuilder<TEntity>>? action = null)
        where TEntity : class
    {
        var mapping = new ReportAsyncMapping<TEntity, IReportMap<TEntity>>
        {
            Accessor = (o, s, b) => accessor((TContext)o, s, b),
            ParentMapping = Mapping,
        };

        MapImpl<TEntity, IReportMap<TEntity>>(id: id, name: name, mapping: mapping, action: action);
    }

    public void Map<TEntity>(
        Func<TContext, Guid?> accessor,
        Action<ReportSchemaBuilder<TEntity>>? action = null)
        where TEntity : class
    {
        var mapping = new ReportMapping<TEntity, IReportMap<TEntity>>
        {
            Accessor = o => accessor((TContext)o),
            ParentMapping = Mapping,
        };

        MapImpl<TEntity, IReportMap<TEntity>>(id: Guid.Empty, name: String.Empty, mapping: mapping, action: action);
    }

    public void Map<TEntity>(
        Guid id,
        string name,
        Func<TContext, List<Guid>> accessor,
        Action<ReportSchemaBuilder<TEntity>>? action = null)
        where TEntity : class
    {
        var mapping = new ReportMultiMapping<TEntity, IReportMap<TEntity>>
        {
            Accessor = o => accessor((TContext)o),
            ParentMapping = Mapping,
        };

        MapImpl<TEntity, IReportMap<TEntity>>(id: id, name: name, mapping: mapping, action: action);
    }

    public void Map<TEntity>(
        Guid id,
        string name,
        Func<TContext, IServiceProvider, IReportMap<TEntity>, Task<List<Guid>>> accessor,
        Action<ReportSchemaBuilder<TEntity>>? action = null)
        where TEntity : class
    {
        var mapping = new ReportAsyncMultiMapping<TEntity, IReportMap<TEntity>>
        {
            Accessor = (o, s, b) => accessor((TContext)o, s, b),
            ParentMapping = Mapping,
        };
        MapImpl<TEntity, IReportMap<TEntity>>(id: id, name: name, mapping: mapping, action: action);
    }

    public void Map<TEntity>(
        Func<TContext, IServiceProvider, IReportMap<TEntity>, Task<List<Guid>>> accessor,
        Action<ReportSchemaBuilder<TEntity>>? action = null)
        where TEntity : class
    {
        var mapping = new ReportAsyncMultiMapping<TEntity, IReportMap<TEntity>>
        {
            Accessor = (o, s, b) => accessor((TContext)o, s, b),
            ParentMapping = Mapping,
        };
        MapImpl<TEntity, IReportMap<TEntity>>(id: Guid.Empty, name: String.Empty, mapping: mapping, action: action);
    }

    public void Map<TEntity, TMap>(
        Func<TContext, IServiceProvider, TMap, Task<List<Guid>>> accessor,
        Action<ReportSchemaBuilder<TEntity>>? action = null)
        where TEntity : class
        where TMap : class, IReportMap<TEntity>
    {
        var mapping = new ReportAsyncMultiMapping<TEntity, TMap>
        {
            Accessor = (o, s, b) => accessor((TContext)o, s, b),
            ParentMapping = Mapping,
        };
        MapImpl<TEntity, TMap>(id: Guid.Empty, name: String.Empty, mapping: mapping, action: action);
    }

    void MapImpl<TEntity, TMap>(
        Guid id,
        string name,
        IReportMapping mapping,
        Action<ReportSchemaBuilder<TEntity>>? action = null)
        where TEntity : class
        where TMap : class, IReportMap<TEntity>
    {
        if (id != Guid.Empty)
        {
            Schema.AddField(new ReportField
            {
                Id = id,
                Name = name,
                Mapping = mapping,
                Accessor = new ReportReferenceAccessor<TMap>(),
                MultiValued = mapping.MultiValued,
                Filterable = true,
                FieldType = ReportFieldType.Reference,
            });
        }

        if (action != null)
            action(new ReportSchemaBuilder<TEntity>(Schema, mapping));
    }

    /////////////////////////////////////////////////////////////////////////////
    // Helpers

    static ReportFieldType MapFieldType(Type clrType)
    {
        var type = UnwrapBaseType(clrType);

        if (type == typeof(string))
            return ReportFieldType.String;

        if (IsNumericType(type))
            return ReportFieldType.Number;

        if (type == typeof(DateOnly))
            return ReportFieldType.Date;

        if (type == typeof(DateTimeOffset))
            return ReportFieldType.DateTime;

        if (type == typeof(bool))
            return ReportFieldType.Boolean;

        throw new InvalidOperationException("Cannot map type " + type.FullName + " to a report field type.");
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

        for (var i = 0; i < input.Length; i++)
        {
            if (i == 0)
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
