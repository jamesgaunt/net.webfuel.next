using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Webfuel
{
    internal partial interface IEventLogRepository
    {
        Task<EventLog> InsertEventLogAsync(EventLog entity);
        Task<EventLog> UpdateEventLogAsync(EventLog entity);
        Task<EventLog> UpdateEventLogAsync(EventLog entity, IEnumerable<string> properties);
        Task<EventLog> UpdateEventLogAsync(EventLog updated, EventLog original);
        Task<EventLog> UpdateEventLogAsync(EventLog updated, EventLog original, IEnumerable<string> properties);
        Task DeleteEventLogAsync(Guid key);
        Task<QueryResult<EventLog>> QueryEventLogAsync(RepositoryQuery query);
        Task<EventLog?> GetEventLogAsync(Guid id);
        Task<EventLog> RequireEventLogAsync(Guid id);
        Task<int> CountEventLogAsync();
        Task<List<EventLog>> SelectEventLogAsync();
        Task<List<EventLog>> SelectEventLogWithPageAsync(int skip, int take);
        Task<List<EventLog>> SelectEventLogByEntityIdAsync(Guid? entityId);
        Task<List<EventLog>> SelectEventLogByTenantIdAsync(Guid? tenantId);
    }
    internal partial class EventLogRepository: IEventLogRepository
    {
        private readonly IRepositoryService RepositoryService;
        private readonly IRepositoryQueryService RepositoryQueryService;
        public EventLogRepository(IRepositoryService repositoryService, IRepositoryQueryService repositoryQueryService)
        {
            RepositoryService = repositoryService;
            RepositoryQueryService = repositoryQueryService;
        }
        public async Task<EventLog> InsertEventLogAsync(EventLog entity)
        {
            return await RepositoryService.ExecuteInsertAsync("InsertEventLog", entity);
        }
        public async Task<EventLog> UpdateEventLogAsync(EventLog entity)
        {
            return await RepositoryService.ExecuteUpdateAsync("UpdateEventLog", entity);
        }
        public async Task<EventLog> UpdateEventLogAsync(EventLog entity, IEnumerable<string> properties)
        {
            return await RepositoryService.ExecuteUpdateAsync("UpdateEventLog", entity, properties);
        }
        public async Task<EventLog> UpdateEventLogAsync(EventLog updated, EventLog original)
        {
            if(updated.Id != original.Id) throw new InvalidOperationException("UpdateEventLogAsync: Entity keys do not match.");
            var _properties = new List<string>();
            if(updated.Level != original.Level) _properties.Add("Level");
            if(updated.Message != original.Message) _properties.Add("Message");
            if(updated.Source != original.Source) _properties.Add("Source");
            if(updated.Detail != original.Detail) _properties.Add("Detail");
            if(updated.Context != original.Context) _properties.Add("Context");
            if(updated.EntityId != original.EntityId) _properties.Add("EntityId");
            if(updated.TenantId != original.TenantId) _properties.Add("TenantId");
            if(updated.IdentityId != original.IdentityId) _properties.Add("IdentityId");
            if(updated.IPAddress != original.IPAddress) _properties.Add("IPAddress");
            if(updated.Exception != original.Exception) _properties.Add("Exception");
            if(updated.RequestUrl != original.RequestUrl) _properties.Add("RequestUrl");
            if(updated.RequestMethod != original.RequestMethod) _properties.Add("RequestMethod");
            if(updated.RequestHeaders != original.RequestHeaders) _properties.Add("RequestHeaders");
            if(_properties.Count == 0) return updated;
            return await RepositoryService.ExecuteUpdateAsync("UpdateEventLog", updated, _properties);
        }
        public async Task<EventLog> UpdateEventLogAsync(EventLog updated, EventLog original, IEnumerable<string> properties)
        {
            if(updated.Id != original.Id) throw new InvalidOperationException("UpdateEventLogAsync: Entity keys do not match.");
            var _properties = new List<string>();
            if(properties.Contains("Level") && updated.Level != original.Level) _properties.Add("Level");
            if(properties.Contains("Message") && updated.Message != original.Message) _properties.Add("Message");
            if(properties.Contains("Source") && updated.Source != original.Source) _properties.Add("Source");
            if(properties.Contains("Detail") && updated.Detail != original.Detail) _properties.Add("Detail");
            if(properties.Contains("Context") && updated.Context != original.Context) _properties.Add("Context");
            if(properties.Contains("EntityId") && updated.EntityId != original.EntityId) _properties.Add("EntityId");
            if(properties.Contains("TenantId") && updated.TenantId != original.TenantId) _properties.Add("TenantId");
            if(properties.Contains("IdentityId") && updated.IdentityId != original.IdentityId) _properties.Add("IdentityId");
            if(properties.Contains("IPAddress") && updated.IPAddress != original.IPAddress) _properties.Add("IPAddress");
            if(properties.Contains("Exception") && updated.Exception != original.Exception) _properties.Add("Exception");
            if(properties.Contains("RequestUrl") && updated.RequestUrl != original.RequestUrl) _properties.Add("RequestUrl");
            if(properties.Contains("RequestMethod") && updated.RequestMethod != original.RequestMethod) _properties.Add("RequestMethod");
            if(properties.Contains("RequestHeaders") && updated.RequestHeaders != original.RequestHeaders) _properties.Add("RequestHeaders");
            if(_properties.Count == 0) return updated;
            return await RepositoryService.ExecuteUpdateAsync("UpdateEventLog", updated, _properties);
        }
        public async Task DeleteEventLogAsync(Guid key)
        {
            await RepositoryService.ExecuteDeleteAsync<EventLog>("DeleteEventLog", key);
        }
        public async Task<QueryResult<EventLog>> QueryEventLogAsync(RepositoryQuery query)
        {
            return await RepositoryQueryService.ExecuteQueryAsync("RepositoryQueryEventLog", query, new EventLogRepositoryAccessor());
        }
        public async Task<EventLog?> GetEventLogAsync(Guid id)
        {
            var sql = @"SELECT * FROM [next].[EventLog] WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            return (await RepositoryService.ExecuteReaderAsync<EventLog>("GetEventLog", sql, parameters)).SingleOrDefault();
        }
        public async Task<EventLog> RequireEventLogAsync(Guid id)
        {
            return await GetEventLogAsync(id) ?? throw new InvalidOperationException("The specified EventLog does not exist");
        }
        public async Task<int> CountEventLogAsync()
        {
            var sql = @"SELECT COUNT(Id) FROM [next].[EventLog]";
            return (int)((await RepositoryService.ExecuteScalarAsync("CountEventLog", sql))!);
        }
        public async Task<List<EventLog>> SelectEventLogAsync()
        {
            var sql = @"SELECT * FROM [next].[EventLog] ORDER BY Id DESC";
            return await RepositoryService.ExecuteReaderAsync<EventLog>("SelectEventLog", sql);
        }
        public async Task<List<EventLog>> SelectEventLogWithPageAsync(int skip, int take)
        {
            var sql = @"SELECT * FROM [next].[EventLog] ORDER BY Id DESC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take),
            };
            return await RepositoryService.ExecuteReaderAsync<EventLog>("SelectEventLogWithPage", sql, parameters);
        }
        public async Task<List<EventLog>> SelectEventLogByEntityIdAsync(Guid? entityId)
        {
            var sql = @"SELECT * FROM [next].[EventLog] WHERE ((EntityId = @EntityId) OR (EntityId IS NULL AND @EntityId IS NULL)) ORDER BY Id DESC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@EntityId", (object?)entityId ?? DBNull.Value),
            };
            return await RepositoryService.ExecuteReaderAsync<EventLog>("SelectEventLogByEntityId", sql, parameters);
        }
        public async Task<List<EventLog>> SelectEventLogByTenantIdAsync(Guid? tenantId)
        {
            var sql = @"SELECT * FROM [next].[EventLog] WHERE ((TenantId = @TenantId) OR (TenantId IS NULL AND @TenantId IS NULL)) ORDER BY Id DESC";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@TenantId", (object?)tenantId ?? DBNull.Value),
            };
            return await RepositoryService.ExecuteReaderAsync<EventLog>("SelectEventLogByTenantId", sql, parameters);
        }
    }
}

