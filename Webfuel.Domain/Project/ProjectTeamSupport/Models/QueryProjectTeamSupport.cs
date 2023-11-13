using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    public class QueryProjectTeamSupport : Query, IRequest<QueryResult<ProjectTeamSupport>>
    {
        public required Guid ProjectId { get; set; }

        public Query ApplyCustomFilters()
        {
            this.Equal(nameof(ProjectTeamSupport.ProjectId), ProjectId);
            return this;
        }
    }
}
