using Microsoft.Data.SqlClient;

namespace Webfuel
{
    public interface IRepositoryMetadata<T> where T : class
    {
        static abstract string DatabaseTable { get; }

        static abstract string DefaultOrderBy { get; }

        static abstract T DataReader(SqlDataReader dr);

        static abstract IEnumerable<SqlParameter> DataWriter(T entity, IEnumerable<string> properties);

        static abstract IEnumerable<string> SelectProperties { get; }

        static abstract IEnumerable<string> InsertProperties { get; }

        static abstract IEnumerable<string> UpdateProperties { get; }

        static abstract void Validate(T entity);
    }
}

