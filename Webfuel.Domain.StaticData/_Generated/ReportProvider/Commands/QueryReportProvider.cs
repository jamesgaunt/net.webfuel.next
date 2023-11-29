using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QueryReportProvider: Query, IRequest<QueryResult<ReportProvider>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(ReportProvider.Name), Search);
            return this;
        }
    }
    internal class QueryReportProviderHandler : IRequestHandler<QueryReportProvider, QueryResult<ReportProvider>>
    {
        private readonly IReportProviderRepository _reportProviderRepository;
        
        
        public QueryReportProviderHandler(IReportProviderRepository reportProviderRepository)
        {
            _reportProviderRepository = reportProviderRepository;
        }
        
        public async Task<QueryResult<ReportProvider>> Handle(QueryReportProvider request, CancellationToken cancellationToken)
        {
            return await _reportProviderRepository.QueryReportProvider(request.ApplyCustomFilters());
        }
    }
}

