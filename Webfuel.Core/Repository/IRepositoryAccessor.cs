namespace Webfuel
{
    public interface IRepositoryAccessor<TEntity> where TEntity : class
    {
        string DatabaseSchema { get; }

        string DatabaseTable { get; }

        string DefaultOrderBy { get; }

        object? GetValue(TEntity entity, string property);

        void SetValue(TEntity entity, string property, object? value);

        TEntity CreateInstance();

        void Validate(TEntity entity);

        IEnumerable<string> InsertProperties { get; }

        IEnumerable<string> UpdateProperties { get; }
    }
}
