using MediatR;

namespace Webfuel.Common
{
    internal class QueryHeartbeatHandler : IRequestHandler<QueryHeartbeat, QueryResult<Heartbeat>>
    {
        private readonly IHeartbeatRepository _heartbeatRepository;

        public QueryHeartbeatHandler(IHeartbeatRepository heartbeatRepository)
        {
            _heartbeatRepository = heartbeatRepository;
        }

        public async Task<QueryResult<Heartbeat>> Handle(QueryHeartbeat request, CancellationToken cancellationToken)
        {
            return await _heartbeatRepository.QueryHeartbeat(request.ApplyCustomFilters());
        }
    }
}
