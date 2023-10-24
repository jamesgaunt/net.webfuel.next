using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    public class QueryProjectSubmission : Query, IRequest<QueryResult<ProjectSubmission>>
    {
        public required Guid ProjectId { get; set; }

        public Query ApplyCustomFilters()
        {
            this.Equal(nameof(ProjectSubmission.ProjectId), ProjectId);
            return this;
        }
    }
}
