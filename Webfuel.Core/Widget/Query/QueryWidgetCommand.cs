using MediatR;

namespace Webfuel
{
    public class QueryWidgetCommand : SearchQuery, IRequest<QueryResult<Widget>>
    {
    }
}
