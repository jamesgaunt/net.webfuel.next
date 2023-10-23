using MediatR;

namespace Webfuel.Domain
{

    internal class QuerySupportRequestHandler : IRequestHandler<QuerySupportRequest, QueryResult<SupportRequest>>
    {
        private readonly ISupportRequestRepository _supportRequestRepository;

        public QuerySupportRequestHandler(ISupportRequestRepository supportRequestRepository)
        {
            _supportRequestRepository = supportRequestRepository;
        }

        public async Task<QueryResult<SupportRequest>> Handle(QuerySupportRequest request, CancellationToken cancellationToken)
        {
            return await _supportRequestRepository.QuerySupportRequest(request.ApplyCustomFilters());
        }
    }
}
