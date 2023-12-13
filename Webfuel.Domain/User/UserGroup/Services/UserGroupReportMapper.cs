using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Reporting;

namespace Webfuel.Domain
{
    [Service(typeof(IReportMapper<UserGroup>))]
    internal class UserGroupReportMapper : IReportMapper<UserGroup>
    {
        private readonly IUserGroupRepository _userGroupRepository;

        public UserGroupReportMapper(IUserGroupRepository userGroupRepository)
        {
            _userGroupRepository = userGroupRepository;
        }

        public async Task<object?> Get(Guid id)
        {
            return await _userGroupRepository.GetUserGroup(id);
        }

        public async Task<QueryResult<object>> Query(Query query)
        {
            var result = await _userGroupRepository.QueryUserGroup(query);

            return new QueryResult<object>
            {
                TotalCount = result.TotalCount,
                Items = result.Items
            };
        }

        public Guid Id(object reference)
        {
            if (reference is not UserGroup entity)
                throw new Exception($"Cannot get id of type {reference.GetType()}");
            return entity.Id;
        }

        public string DisplayName(object reference)
        {
            if (reference is not UserGroup entity)
                throw new Exception($"Cannot get display name of type {reference.GetType()}");
            return entity.Name;
        }
    }
}
