﻿using System;
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

        public ReportBuilder GetReportBuilder(ReportRequest request)
        {
            return new StandardReportBuilder(request);
        }

        public async Task<IEnumerable<object>> QueryItems(int skip, int take)
        {
            var result = await _userRepository.QueryUser(new Query { Skip = skip, Take = take }, countTotal: false);
            return result.Items;
        }

        public async Task<int> GetTotalCount()
        {
            var result = await _userRepository.QueryUser(new Query(), selectItems: false, countTotal: true);
            return result.TotalCount;
        }

        public ReportSchema Schema
        {
            get
            {
                if (_schema == null)
                {
                    var builder = new ReportSchemaBuilder<User>(ReportProviderEnum.User);

                    builder.AddProperty(Guid.Parse("c077475e-16cd-484a-9e61-516550b92143"), p => p.Email);
                    builder.AddProperty(Guid.Parse("21be5330-cdfd-4a73-9d42-2c1d6ea258ae"), p => p.Title);
                    builder.AddProperty(Guid.Parse("54f8f29e-926b-4b26-a0d5-cb08f4fcdf09"), p => p.FirstName);
                    builder.AddProperty(Guid.Parse("755f3b11-3df9-4bd9-97ba-883a45f0fcf5"), p => p.LastName);

                    builder.AddReference<IUserGroupReferenceProvider>(Guid.Parse("21c29efb-ea5a-43c8-9fec-fbad422681cc"), p => p.UserGroupId);

                    builder.AddProperty(Guid.Parse("caebc906-c010-4b77-b139-cac2d68e7a3e"), p => p.RSSJobTitle);
                    builder.AddProperty(Guid.Parse("fe6019ca-9a97-4b3d-b1a4-7b361f1a64ed"), p => p.UniversityJobTitle);
                    builder.AddProperty(Guid.Parse("3660f9d3-9775-4804-9234-3bb64146d006"), p => p.ProfessionalBackground);
                    builder.AddProperty(Guid.Parse("9103d85e-a73b-481f-85bb-306c201788c0"), p => p.Specialisation);

                    builder.AddReferenceList<IUserDisciplineReferenceProvider>(Guid.Parse("b104028e-7449-4ce3-b801-557762170fa7"), p => p.DisciplineIds);

                    builder.AddProperty(Guid.Parse("3beb8a42-a18d-4c6e-a750-5c955a831948"), p => p.StartDateForRSS);
                    builder.AddProperty(Guid.Parse("15a4be71-6860-48b7-b21a-25f4d666fd1c"), p => p.EndDateForRSS);
                    builder.AddProperty(Guid.Parse("a1733340-097c-49ab-b0ba-f45a1f428cfc"), p => p.FullTimeEquivalentForRSS);

                    builder.AddReference<ISiteReferenceProvider>(Guid.Parse("59fe53ed-0b1f-435a-98e8-a02abaa90648"), p => p.SiteId);

                    builder.AddProperty(Guid.Parse(Guid.Parse("6fb3950e-5db7-4941-b4e2-6550705528ee").ToString()), p => p.Hidden);
                    builder.AddProperty(Guid.Parse(Guid.Parse("9f8e536b-1249-4b30-9596-1c811a7c83b6").ToString()), p => p.Disabled);

                    _schema = builder.Schema;
                }
                return _schema;
            }
        }

        static ReportSchema? _schema = null;
    }
}