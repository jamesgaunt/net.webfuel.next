using MediatR;

namespace Webfuel
{
    public class QueryWidget : SearchQuery, IRequest<QueryResult<WidgetQueryView>>
    {
    }

    internal class QueryWidgetHandler : IRequestHandler<QueryWidget, QueryResult<WidgetQueryView>>
    {
        private readonly IWidgetQueryViewRepository _widgetQueryViewRepository;

        public QueryWidgetHandler(IWidgetQueryViewRepository widgetQueryViewRepository)
        {
            _widgetQueryViewRepository = widgetQueryViewRepository;
        }

        public async Task<QueryResult<WidgetQueryView>> Handle(QueryWidget request, CancellationToken cancellationToken)
        {
            var q = request.ToRepositoryQuery();

            q.All(a => a.Contains(nameof(WidgetQueryView.Name), request?.Filter?.Search));

            return await _widgetQueryViewRepository.QueryWidgetQueryViewAsync(q);
        }
    }
}
