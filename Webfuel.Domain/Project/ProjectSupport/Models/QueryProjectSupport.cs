using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    public class QueryProjectSupport : Query, IRequest<QueryResult<ProjectSupport>>
    {
        public required Guid ProjectId { get; set; }

        public Query ApplyCustomFilters()
        {
            this.Equal(nameof(ProjectSupport.ProjectId), ProjectId);
            return this;
        }
    }
}
