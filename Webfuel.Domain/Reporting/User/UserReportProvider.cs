using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Common;
using Webfuel.Domain.StaticData;
using Webfuel.Reporting;

namespace Webfuel.Domain
{
    public interface IUserReportProvider: IReportProvider
    {
    }

    [Service(typeof(IUserReportProvider), typeof(IReportProvider))]
    internal class UserReportProvider : IUserReportProvider
    {
        public Guid Id => ReportProviderEnum.User;

        public Task<ReportSchema> GetReportSchema()
        {
            return Task.FromResult(UserReportSchema.Schema);
        }

        public Task<ReportBuilder> InitialiseReport(ReportRequest request)
        {
            return Task.FromResult<ReportBuilder>(new UserReportBuilder(request));
        }
    }
}
