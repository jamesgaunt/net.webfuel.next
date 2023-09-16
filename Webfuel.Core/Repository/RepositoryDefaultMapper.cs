using Microsoft.Data.SqlClient;
using System.Text;

namespace Webfuel
{
    public class RepositoryDefaultMapper<TEntity> : IRepositoryMapper<TEntity> where TEntity : class
    {
        private readonly IRepositoryAccessor<TEntity> Accessor;

        public RepositoryDefaultMapper(IRepositoryAccessor<TEntity> accessor)
        {
            Accessor = accessor;
        }

        public TEntity ActivateEntity(SqlDataReader dr)
        {
            var t = Accessor.CreateInstance();
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var name = dr.GetName(i);
                Accessor.SetValue(t, name, value);
            }
            return (TEntity)t;
        }

        public async virtual Task<TEntity> ExecuteInsertAsync(string spanName, IRepositoryService repositoryService, TEntity entity, IEnumerable<string>? properties = null)
        {
            var command = BuildInsertCommand(entity, properties);
            await repositoryService.ExecuteNonQueryAsync(spanName, command.sql, command.parameters);
            return entity;
        }

        public async virtual Task<TEntity> ExecuteUpdateAsync(string spanName, IRepositoryService repositoryService, TEntity entity, IEnumerable<string>? properties = null)
        {
            var command = BuildUpdateCommand(entity, properties);
            await repositoryService.ExecuteNonQueryAsync(spanName, command.sql, command.parameters);
            return entity;
        }

        public async virtual Task ExecuteDeleteAsync(string spanName, IRepositoryService repositoryService, object key)
        {
            var command = BuildDeleteCommand(key);
            await repositoryService.ExecuteNonQueryAsync(spanName, command.sql, command.parameters);
        }

        // Insert

        protected virtual (string sql, IEnumerable<SqlParameter> parameters) BuildInsertCommand(TEntity entity, IEnumerable<string>? properties = null)
        {
            InsertPreamble(entity);
            properties = properties ?? Accessor.InsertProperties;
            var parameters = ExtractParameters(entity, properties);
            var sql = InsertSQL(properties);
            return (sql, parameters);
        }

        protected virtual void InsertPreamble(TEntity entity)
        {
            var existingId = (Guid)Accessor.GetValue(entity, "Id")!;
            if (existingId == Guid.Empty)
                Accessor.SetValue(entity, "Id", GuidGenerator.NewComb());
            Accessor.Validate(entity);
        }

        protected virtual string InsertSQL(IEnumerable<string> properties)
        {
            var sb = new StringBuilder();
            sb.Append($"INSERT INTO [{Accessor.DatabaseSchema}].[{Accessor.DatabaseTable}] (");
            sb.Append(String.Join(", ", properties.Select(p => $"[{p}]")));
            sb.Append(") VALUES (");
            sb.Append(String.Join(", ", properties.Select(p => $"@{p}")));
            sb.Append(")");
            return sb.ToString();
        }

        // Update

        protected virtual (string sql, IEnumerable<SqlParameter> parameters) BuildUpdateCommand(TEntity entity, IEnumerable<string>? properties = null)
        {
            UpdatePreamble(entity);
            properties = properties ?? Accessor.UpdateProperties;
            var parameters = ExtractParameters(entity, properties);
            var sql = UpdateSQL(properties);
            return (sql, parameters);
        }

        protected virtual void UpdatePreamble(TEntity entity)
        {
            Accessor.Validate(entity);
        }

        protected virtual string UpdateSQL(IEnumerable<string> properties)
        {
            var sb = new StringBuilder();
            sb.Append($"UPDATE [{Accessor.DatabaseSchema}].[{Accessor.DatabaseTable}] SET ");
            sb.Append(String.Join(", ", properties.Where(p => p != "Id").Select(p => $"[{p}] = @{p}")));
            sb.Append($" WHERE [Id] = @Id");
            return sb.ToString();
        }

        // Delete

        protected virtual (string sql, IEnumerable<SqlParameter> parameters) BuildDeleteCommand(object key)
        {
            return (DeleteSQL(), new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = key } });
        }

        protected virtual string DeleteSQL()
        {
            var sb = new StringBuilder();
            sb.Append($"DELETE FROM [{Accessor.DatabaseSchema}].[{Accessor.DatabaseTable}] WHERE Id = @Id");
            return sb.ToString();
        }

        // Helpers

        protected virtual List<SqlParameter> ExtractParameters(TEntity entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter>();
            foreach (var property in properties)
            {
                if (property == "Id")
                    continue;
                result.Add(new SqlParameter { ParameterName = $"@{property}", Value = Accessor.GetValue(entity, property) ?? DBNull.Value });
            }
            result.Add(new SqlParameter { ParameterName = $"@Id", Value = Accessor.GetValue(entity, "Id") ?? DBNull.Value });
            return result;
        }
    }
}
