using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Reporting;

namespace Webfuel.Domain
{
    public interface IUserGroupReferenceProvider: IReportReferenceProvider
    {
    }

    [Service(typeof(IUserGroupReferenceProvider))]
    internal class UserGroupReferenceProvider : IUserGroupReferenceProvider
    {
        private readonly IUserGroupRepository _userGroupRepository;

        public UserGroupReferenceProvider(IUserGroupRepository userGroupRepository)
        {
            _userGroupRepository = userGroupRepository;
        }

        public async Task<ReportReference?> GetReportReference(Guid id)
        {
            var userGroup = await _userGroupRepository.GetUserGroup(id);
            if (userGroup == null)
                return null;

            return new ReportReference
            {
                Id = userGroup.Id,
                Name = userGroup.Name,
                Entity = userGroup,
            };
        }

        public async Task<QueryResult<ReportReference>> QueryReportReference(Query query)
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
                }).ToList(),
            };
        }
    }
}
