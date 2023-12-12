using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Reporting;

namespace Webfuel.Domain
{
    [Service(typeof(IReportReferenceProvider<UserGroup>))]
    internal class UserGroupReportReferenceProvider : IReportReferenceProvider<UserGroup>
    {
        private readonly IUserGroupRepository _userGroupRepository;

        public UserGroupReportReferenceProvider(IUserGroupRepository userGroupRepository)
        {
            _userGroupRepository = userGroupRepository;
        }

        public async Task<ReportReference?> Get(Guid id)
        {
            var entity = await _userGroupRepository.GetUserGroup(id);
            if (entity == null)
                return null;

            return new ReportReference
            {
                Id = entity.Id,
                Name = entity.Name,
                Entity = entity
            };
        }

        public async Task<QueryResult<ReportReference>> Query(Query query)
        {
            var result = await _userGroupRepository.QueryUserGroup(query);

            return new QueryResult<ReportReference>
            {
                TotalCount = result.TotalCount,
                Items = result.Items.Select(x => new ReportReference
                {
                    Id = x.Id,
                    Name = x.Name,
                    Entity = x
                }).ToList()
            };
        }
    }
}
