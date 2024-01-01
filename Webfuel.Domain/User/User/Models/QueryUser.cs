using MediatR;

namespace Webfuel.Domain
{
    public class QueryUser : Query, IRequest<QueryResult<User>>
    {
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(User.FullName), Search);
            return this;
        }
    }
}
