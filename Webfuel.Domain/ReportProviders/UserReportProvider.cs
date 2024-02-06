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
        private readonly IUserRepository _userRepository;

        public UserReportProvider(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Guid Id => ReportProviderEnum.User;

        public ReportBuilderBase GetReportBuilder(ReportRequest request)
        {
            return new ReportBuilder(request);
        }

        public async Task<IEnumerable<object>> QueryItems(Query query)
        {
            var result = await _userRepository.QueryUser(query, countTotal: false);
            return result.Items;
        }

        public async Task<int> GetTotalCount(Query query)
        {
            var result = await _userRepository.QueryUser(query, selectItems: false, countTotal: true);
            return result.TotalCount;
        }

        // Schema

        public ReportSchema Schema
        {
            get
            {
                if (_schema == null)
                {
                    var bldr = ReportSchemaBuilder<User>.Create(ReportProviderEnum.User);

                    bldr.Add(Guid.Parse("c077475e-16cd-484a-9e61-516550b92143"), "Email", p => p.Email);
                    bldr.Add(Guid.Parse("21be5330-cdfd-4a73-9d42-2c1d6ea258ae"), "Title", p => p.Title);
                    bldr.Add(Guid.Parse("54f8f29e-926b-4b26-a0d5-cb08f4fcdf09"), "First Name", p => p.FirstName);
                    bldr.Add(Guid.Parse("755f3b11-3df9-4bd9-97ba-883a45f0fcf5"), "Last Name", p => p.LastName);

                    bldr.Map<UserGroup>(Guid.Parse("fafbe3e6-14f2-42c3-b702-88d0178e720b"), "User Group", p => p.UserGroupId);

                    bldr.Add(Guid.Parse("caebc906-c010-4b77-b139-cac2d68e7a3e"), "RSS Job Title", p => p.RSSJobTitle);
                    bldr.Add(Guid.Parse("fe6019ca-9a97-4b3d-b1a4-7b361f1a64ed"), "University Job Title", p => p.UniversityJobTitle);
                    bldr.Add(Guid.Parse("3660f9d3-9775-4804-9234-3bb64146d006"), "Professional Background", p => p.ProfessionalBackground);
                    bldr.Add(Guid.Parse("9103d85e-a73b-481f-85bb-306c201788c0"), "Specialisation", p => p.Specialisation);

                    bldr.Add(Guid.Parse("3beb8a42-a18d-4c6e-a750-5c955a831948"), "RSS Start Date", p => p.StartDateForRSS);
                    bldr.Add(Guid.Parse("15a4be71-6860-48b7-b21a-25f4d666fd1c"), "RSS End Date", p => p.EndDateForRSS);
                    bldr.Add(Guid.Parse("a1733340-097c-49ab-b0ba-f45a1f428cfc"), "RSS FTE", p => p.FullTimeEquivalentForRSS);

                    bldr.Map<UserDiscipline>(Guid.Parse("32a8c15f-b8a6-4569-86c4-0b23735e38c9"), "Disciplines", p => p.DisciplineIds);

                    bldr.Map<Site>(Guid.Parse("59fe53ed-0b1f-435a-98e8-a02abaa90648"), "Site", p => p.SiteId);
   
                    bldr.Add(Guid.Parse("6fb3950e-5db7-4941-b4e2-6550705528ee"), "Hidden", p => p.Hidden);
                    bldr.Add(Guid.Parse("9f8e536b-1249-4b30-9596-1c811a7c83b6"), "Disabled", p => p.Disabled);

                    _schema = bldr.Schema;
                }
                return _schema;
            }
        }

        static ReportSchema? _schema = null;
    }
}
