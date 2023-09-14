using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webfuel;

namespace Webfuel.App.Api
{
    [TypefuelController]
    [Route("api/[controller]")]
    public class EventLogController : ControllerBase
    {
        private readonly IEventLogService EventLogService;

        public EventLogController(IEventLogService eventLogService)
        {
            EventLogService = eventLogService;
        }

        [HttpPost("")]
        public async Task<EventLog> Insert([FromBody] EventLog eventLog)
        {
            return await EventLogService.InsertEventLogAsync(eventLog: eventLog);
        }

        [HttpPost("query")]
        [TypefuelAction(RetryCount = 3)]
        public async Task<RepositoryQueryResult<EventLog>> Query([FromBody] RepositoryQuery query)
        {
            return await EventLogService.QueryEventLogAsync(query);
        }
    }
}