using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Common
{
    internal partial interface IEmailLogRepository
    {
        Task<EmailLog> InsertEmailLog(EmailLog entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<EmailLog> UpdateEmailLog(EmailLog entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<EmailLog> UpdateEmailLog(EmailLog updated, EmailLog original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteEmailLog(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<EmailLog>> QueryEmailLog(Query query, bool countTotal = true);
        Task<EmailLog?> GetEmailLog(Guid id);
        Task<EmailLog> RequireEmailLog(Guid id);
        Task<int> CountEmailLog();
        Task<List<EmailLog>> SelectEmailLog();
        Task<List<EmailLog>> SelectEmailLogWithPage(int skip, int take);
        Task<List<EmailLog>> SelectEmailLogByEntityId(Guid entityId);
    }
    [Service(typeof(IEmailLogRepository))]
    internal partial class EmailLogRepository: IEmailLogRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public EmailLogRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<EmailLog> InsertEmailLog(EmailLog entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            EmailLogMetadata.Validate(entity);
            var sql = EmailLogMetadata.InsertSQL();
            var parameters = EmailLogMetadata.ExtractParameters(entity, EmailLogMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<EmailLog> UpdateEmailLog(EmailLog entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            EmailLogMetadata.Validate(entity);
            var sql = EmailLogMetadata.UpdateSQL();
            var parameters = EmailLogMetadata.ExtractParameters(entity, EmailLogMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<EmailLog> UpdateEmailLog(EmailLog updated, EmailLog original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateEmailLog(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteEmailLog(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = EmailLogMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<EmailLog>> QueryEmailLog(Query query, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<EmailLog, EmailLogMetadata>(query, countTotal);
        }
        public async Task<EmailLog?> GetEmailLog(Guid id)
        {
            var sql = @"SELECT * FROM [EmailLog] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<EmailLog, EmailLogMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<EmailLog> RequireEmailLog(Guid id)
        {
            return await GetEmailLog(id) ?? throw new InvalidOperationException("The specified EmailLog does not exist");
        }
        public async Task<int> CountEmailLog()
        {
            var sql = @"SELECT COUNT(Id) FROM [EmailLog]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<EmailLog>> SelectEmailLog()
        {
            var sql = @"SELECT * FROM [EmailLog] ORDER BY Id DESC";
            return await _connection.ExecuteReader<EmailLog, EmailLogMetadata>(sql);
        }
        public async Task<List<EmailLog>> SelectEmailLogWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [EmailLog] ORDER BY Id DESC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<EmailLog, EmailLogMetadata>(sql, parameters);
        }
        public async Task<List<EmailLog>> SelectEmailLogByEntityId(Guid entityId)
        {
            var sql = @"SELECT * FROM [EmailLog] WHERE EntityId = @EntityId ORDER BY Id DESC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@EntityId", entityId),
            };
            return await _connection.ExecuteReader<EmailLog, EmailLogMetadata>(sql, parameters);
        }
    }
}

