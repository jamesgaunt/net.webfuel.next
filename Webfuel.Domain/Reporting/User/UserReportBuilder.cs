using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Reporting;

namespace Webfuel.Domain
{
    internal class UserReportBuilder : SchemaReportBuilder
    {
        public UserReportBuilder(ReportRequest request):
            base(UserReportSchema.Schema, request, new UserReportQuery())
        {
        }
    }
}
