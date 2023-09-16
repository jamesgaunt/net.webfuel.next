using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{
    public interface IEventLogService
    {
        Task<EventLog> InsertEventLogAsync(EventLog eventLog);

        Task<QueryResult<EventLog>> QueryEventLogAsync(SimpleQuery query);
    }

    [ServiceImplementation(typeof(IEventLogService))]
    internal class EventLogService : IEventLogService
    {
        private readonly IEventLogRepository EventLogRepository;

        public EventLogService(IEventLogRepository eventLogRepository)
        {
            EventLogRepository = eventLogRepository;
        }

        public async Task<EventLog> InsertEventLogAsync(EventLog eventLog)
        {
            return await EventLogRepository.InsertEventLogAsync(eventLog);
        }

        public async Task<QueryResult<EventLog>> QueryEventLogAsync(SimpleQuery query)
        {
            return await EventLogRepository.QueryEventLogAsync(query.ToRepositoryQuery());
        }
    }
}