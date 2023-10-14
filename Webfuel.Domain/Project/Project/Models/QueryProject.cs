using MediatR;

namespace Webfuel.Domain
{
    public class QueryProject : Query, IRequest<QueryResult<Project>>
    {
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(Project.Title), Search);
            return this;
        }
    }
}
