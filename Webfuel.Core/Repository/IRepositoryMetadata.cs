using Microsoft.Data.SqlClient;
using System.Text;

namespace Webfuel
{
    public interface IRepositoryMetadata<T> where T : class
    {
        static abstract string DatabaseTable { get; }

        static abstract string DefaultOrderBy { get; }

        static abstract string InsertSQL(IEnumerable<string>? properties = null);

        static abstract string UpdateSQL(IEnumerable<string>? properties = null);

        static abstract string DeleteSQL();

        static abstract T DataReader(SqlDataReader dr);

        static abstract List<SqlParameter> ExtractParameters(T entity, IEnumerable<string> properties);

        static abstract IEnumerable<string> SelectProperties { get; }

        static abstract IEnumerable<string> InsertProperties { get; }

        static abstract IEnumerable<string> UpdateProperties { get; }

        static abstract void Validate(T entity);
    }

    public static class RepositoryMetadataDefaults
    {
        public static string InsertSQL<TEntity, TEntityMetadata>(IEnumerable<string> properties)
            where TEntity : class
            where TEntityMetadata : IRepositoryMetadata<TEntity>
        {
            var sb = new StringBuilder();
            sb.Append($"INSERT INTO [{TEntityMetadata.DatabaseTable}] (");
            sb.Append(String.Join(", ", properties.Select(p => $"[{p}]")));
            sb.Append(") VALUES (");
            sb.Append(String.Join(", ", properties.Select(p => $"@{p}")));
            sb.Append(")");
            return sb.ToString();
        }

        public static string UpdateSQL<TEntity, TEntityMetadata>(IEnumerable<string> properties)
            where TEntity : class
            where TEntityMetadata : IRepositoryMetadata<TEntity>
        {
            var sb = new StringBuilder();
            sb.Append($"UPDATE [{TEntityMetadata.DatabaseTable}] SET ");
            sb.Append(String.Join(", ", properties.Where(p => p != "Id").Select(p => $"[{p}] = @{p}")));
            sb.Append($" WHERE [Id] = @Id");
            return sb.ToString();
        }

        public static string DeleteSQL<TEntity, TEntityMetadata>()
            where TEntity : class
            where TEntityMetadata : IRepositoryMetadata<TEntity>
        {
            var sb = new StringBuilder();
            sb.Append($"DELETE FROM [{TEntityMetadata.DatabaseTable}] WHERE Id = @Id");
            return sb.ToString();
        }
    }
}

