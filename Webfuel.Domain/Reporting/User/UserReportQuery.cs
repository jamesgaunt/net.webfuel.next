using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Reporting;

namespace Webfuel.Domain
{
    internal class UserReportQuery : ReportQuery
    {
        public override async Task<IReadOnlyList<object>> GetItems(int skip, int take, IServiceProvider services)
        {
            Query.Skip = skip;
            Query.Take = take;

            var result = await services.GetRequiredService<IUserRepository>().QueryUser(Query, countTotal: false);
            return result.Items.Select(p => new UserReportContext(p, services)).ToList();
        }

        public override async Task<int> GetTotalCount(IServiceProvider services)
        {
            Query.Skip = 0;
            Query.Take = 0;

            var result = await services.GetRequiredService<IUserRepository>().QueryUser(Query, selectItems: false, countTotal: true);
            return result.TotalCount;
        }
    }
}
