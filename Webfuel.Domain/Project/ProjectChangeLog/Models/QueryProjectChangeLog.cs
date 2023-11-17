using MediatR;

namespace Webfuel.Domain
{
    public class QueryProjectChangeLog : Query, IRequest<QueryResult<ProjectChangeLog>>
    {
        public Query ApplyCustomFilters()
        {
            this.Equal(nameof(ProjectChangeLog.ProjectId), ProjectId);
            return this;
        }

        public required Guid ProjectId { get; set; }
    }
}
