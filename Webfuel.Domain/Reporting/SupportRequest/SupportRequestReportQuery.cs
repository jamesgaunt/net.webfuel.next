﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Reporting;

namespace Webfuel.Domain
{
    internal class SupportRequestReportQuery : ReportQuery
    {
        public override async Task<IReadOnlyList<object>> GetItems(int skip, int take, IServiceProvider services)
        {
            Query.Skip = skip;
            Query.Take = take;

            var result = await services.GetRequiredService<ISupportRequestRepository>().QuerySupportRequest(Query, countTotal: false);
            return result.Items;
        }

        public override async Task<int> GetTotalCount(IServiceProvider services)
        {
            Query.Skip = 0;
            Query.Take = 0;

            var result = await services.GetRequiredService<ISupportRequestRepository>().QuerySupportRequest(Query, selectItems: false, countTotal: true);
            return result.TotalCount;
        }
    }
}