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
            if(_properties.Count == 0) return updated;
            return await RepositoryService.ExecuteUpdateAsync("UpdateEventLog", updated, _properties);
        }
        public async Task<EventLog> UpdateEventLogAsync(EventLog updated, EventLog original, IEnumerable<string> properties)
        {
            if(updated.Id != original.Id) throw new InvalidOperationException("UpdateEventLogAsync: Entity keys do not match.");
            var _properties = new List<string>();
            if(properties.Contains("Level") && updated.Level != original.Level) _properties.Add("Level");
            if(properties.Contains("Message") && updated.Message != original.Message) _properties.Add("Message");
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
    }
}

