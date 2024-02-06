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
    public interface IUserActivityReportProvider: IReportProvider
    {
    }

    [Service(typeof(IUserActivityReportProvider), typeof(IReportProvider))]
    internal class UserActivityReportProvider : IUserActivityReportProvider
    {
        private readonly IUserActivityRepository _userRepository;

        public UserActivityReportProvider(IUserActivityRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Guid Id => ReportProviderEnum.UserActivity;

        public ReportBuilderBase GetReportBuilder(ReportRequest request)
        {
            return new ReportBuilder(request);
        }

        public async Task<IEnumerable<object>> QueryItems(Query query)
        {
            var result = await _userRepository.QueryUserActivity(query, countTotal: false);
            return result.Items;
        }

        public async Task<int> GetTotalCount(Query query)
        {
            var result = await _userRepository.QueryUserActivity(query, selectItems: false, countTotal: true);
            return result.TotalCount;
        }

        // Schema

        public ReportSchema Schema
        {
            get
            {
                if (_schema == null)
                {
                    var bldr = ReportSchemaBuilder<UserActivity>.Create(ReportProviderEnum.UserActivity);

                    bldr.Map<User>(Guid.Parse("1dc67011-daf2-439b-9b1a-af433748aff0"), "User", p => p.UserId);
                    bldr.Add(Guid.Parse("2a59499b-03ba-4fce-a65e-e892506ac57f"), "Date", p => p.Date);
                    bldr.Add(Guid.Parse("d4563097-7d19-475f-a77b-a4b01d6ad5d8"), "Description", p => p.Description);
                    bldr.Add(Guid.Parse("b6fe2da7-00f6-401c-9264-095058c64a30"), "Work Time In Hours", p => p.WorkTimeInHours);
                    bldr.Map<WorkActivity>(Guid.Parse("5bcc5898-1492-4ad8-82f3-4720216aa184"), "Work Activity", p => p.WorkActivityId);
                    bldr.Map<Project>(Guid.Parse("2d15720f-7d8a-4eae-99d0-8bcba08cd296"), "Project", p => p.ProjectId);

                    _schema = bldr.Schema;
                }
                return _schema;
            }
        }

        static ReportSchema? _schema = null;
    }
}
