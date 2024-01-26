using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel.Common
{
    internal partial interface IEmailTemplateRepository
    {
        Task<EmailTemplate> InsertEmailTemplate(EmailTemplate entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<EmailTemplate> UpdateEmailTemplate(EmailTemplate entity, RepositoryCommandBuffer? commandBuffer = null);
        Task<EmailTemplate> UpdateEmailTemplate(EmailTemplate updated, EmailTemplate original, RepositoryCommandBuffer? commandBuffer = null);
        Task DeleteEmailTemplate(Guid key, RepositoryCommandBuffer? commandBuffer = null);
        Task<QueryResult<EmailTemplate>> QueryEmailTemplate(Query query, bool selectItems = true, bool countTotal = true);
        Task<EmailTemplate?> GetEmailTemplate(Guid id);
        Task<EmailTemplate> RequireEmailTemplate(Guid id);
        Task<int> CountEmailTemplate();
        Task<List<EmailTemplate>> SelectEmailTemplate();
        Task<List<EmailTemplate>> SelectEmailTemplateWithPage(int skip, int take);
        Task<EmailTemplate?> GetEmailTemplateByName(string name);
        Task<EmailTemplate> RequireEmailTemplateByName(string name);
    }
    [Service(typeof(IEmailTemplateRepository))]
    internal partial class EmailTemplateRepository: IEmailTemplateRepository
    {
        private readonly IRepositoryConnection _connection;
        
        public EmailTemplateRepository(IRepositoryConnection connection)
        {
            _connection = connection;
        }
        public async Task<EmailTemplate> InsertEmailTemplate(EmailTemplate entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            if (entity.Id == Guid.Empty) entity.Id = GuidGenerator.NewComb();
            EmailTemplateMetadata.Validate(entity);
            var sql = EmailTemplateMetadata.InsertSQL();
            var parameters = EmailTemplateMetadata.ExtractParameters(entity, EmailTemplateMetadata.InsertProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<EmailTemplate> UpdateEmailTemplate(EmailTemplate entity, RepositoryCommandBuffer? commandBuffer = null)
        {
            EmailTemplateMetadata.Validate(entity);
            var sql = EmailTemplateMetadata.UpdateSQL();
            var parameters = EmailTemplateMetadata.ExtractParameters(entity, EmailTemplateMetadata.UpdateProperties);
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
            return entity;
        }
        public async Task<EmailTemplate> UpdateEmailTemplate(EmailTemplate updated, EmailTemplate original, RepositoryCommandBuffer? commandBuffer = null)
        {
            await UpdateEmailTemplate(updated, commandBuffer);
            return updated;
        }
        public async Task DeleteEmailTemplate(Guid id, RepositoryCommandBuffer? commandBuffer = null)
        {
            var sql = EmailTemplateMetadata.DeleteSQL();
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", Value = id } };
            await _connection.ExecuteNonQuery(sql, parameters, commandBuffer);
        }
        public async Task<QueryResult<EmailTemplate>> QueryEmailTemplate(Query query, bool selectItems = true, bool countTotal = true)
        {
            return await _connection.ExecuteQuery<EmailTemplate, EmailTemplateMetadata>(query, selectItems: selectItems, countTotal: countTotal);
        }
        public async Task<EmailTemplate?> GetEmailTemplate(Guid id)
        {
            var sql = @"SELECT * FROM [EmailTemplate] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await _connection.ExecuteReader<EmailTemplate, EmailTemplateMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<EmailTemplate> RequireEmailTemplate(Guid id)
        {
            return await GetEmailTemplate(id) ?? throw new InvalidOperationException("The specified EmailTemplate does not exist");
        }
        public async Task<int> CountEmailTemplate()
        {
            var sql = @"SELECT COUNT(Id) FROM [EmailTemplate]";
            return (int)((await _connection.ExecuteScalar(sql))!);
        }
        public async Task<List<EmailTemplate>> SelectEmailTemplate()
        {
            var sql = @"SELECT * FROM [EmailTemplate] ORDER BY Id ASC";
            return await _connection.ExecuteReader<EmailTemplate, EmailTemplateMetadata>(sql);
        }
        public async Task<List<EmailTemplate>> SelectEmailTemplateWithPage(int skip, int take)
        {
            var sql = @"SELECT * FROM [EmailTemplate] ORDER BY Id ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await _connection.ExecuteReader<EmailTemplate, EmailTemplateMetadata>(sql, parameters);
        }
        public async Task<EmailTemplate?> GetEmailTemplateByName(string name)
        {
            var sql = @"SELECT * FROM [EmailTemplate] WHERE Name = @Name ORDER BY Id ASC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", name),
            };
            return (await _connection.ExecuteReader<EmailTemplate, EmailTemplateMetadata>(sql, parameters)).SingleOrDefault();
        }
        public async Task<EmailTemplate> RequireEmailTemplateByName(string name)
        {
            return await GetEmailTemplateByName(name) ?? throw new InvalidOperationException("The specified EmailTemplate does not exist");
        }
    }
}

