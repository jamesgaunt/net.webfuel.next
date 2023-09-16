using MediatR;

namespace Webfuel
{
    internal class QueryWidgetCommandHandler : IRequestHandler<QueryWidgetCommand, QueryResult<Widget>>
    {
        private readonly IWidgetRepository _widgetRepository;

        public QueryWidgetCommandHandler(IWidgetRepository widgetRepository)
        {
            _widgetRepository = widgetRepository;
        }

        public async Task<QueryResult<Widget>> Handle(QueryWidgetCommand request, CancellationToken cancellationToken)
        {
            var q = request.ToRepositoryQuery();

            q.All(a => a.Contains(nameof(Widget.Name), request?.Filter?.Search));

            return await _widgetRepository.QueryWidgetAsync(q);
        }
    }
}
