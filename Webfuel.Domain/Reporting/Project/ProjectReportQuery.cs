using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Reporting;

namespace Webfuel.Domain
{
    internal class ProjectReportQuery : ReportQuery
    {
        public override async Task<IEnumerable<object>> GetItems(int skip, int take, IServiceProvider services)
        {
            Query.Skip = skip;
            Query.Take = take;

            var result = await services.GetRequiredService<IProjectRepository>().QueryProject(Query, countTotal: false);
            return result.Items;
        }

        public override async Task<int> GetTotalCount(IServiceProvider services)
        {
            Query.Skip = 0;
            Query.Take = 0;

            var result = await services.GetRequiredService<IProjectRepository>().QueryProject(Query, countTotal: true);
            return result.TotalCount;
        }
    }
}
