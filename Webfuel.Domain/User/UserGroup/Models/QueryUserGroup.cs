using MediatR;

namespace Webfuel.Domain;

public class QueryUserGroup : Query, IRequest<QueryResult<UserGroup>>
{
    public Query ApplyCustomFilters()
    {
        this.Contains(nameof(UserGroup.Name), Search);
        return this;
    }
}
