namespace Webfuel
{
    public interface IRepositoryAccessor<TEntity> where TEntity : class
    {
        string DatabaseTable { get; } // Static Metadata

        string DefaultOrderBy { get; } // Static Metadata

        object? GetValue(TEntity entity, string property);

        void SetValue(TEntity entity, string property, object? value);

        TEntity CreateInstance();

        void Validate(TEntity entity);  // Static Metadata

        IEnumerable<string> InsertProperties { get; } // Static Metadata

        IEnumerable<string> UpdateProperties { get; } // Static Metadata
    }
}
