using MediatR;

namespace Webfuel.Domain
{
    public class QueryUserLogin : Query, IRequest<QueryResult<UserLogin>>
    {
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(UserLogin.Email), Search);
            return this;
        }
    }
}
