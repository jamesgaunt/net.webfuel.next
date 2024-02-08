using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Reporting;

namespace Webfuel.Domain
{
    [Service(typeof(IReportMap<UserGroup>))]
    internal class UserGroupReportMap : IReportMap<UserGroup>
    {
        private readonly IUserGroupRepository _userGroupRepository;

        public UserGroupReportMap(IUserGroupRepository userGroupRepository)
        {
            _userGroupRepository = userGroupRepository;
        }

        public async Task<object?> Get(Guid id)
        {
            return await _userGroupRepository.GetUserGroup(id);
        }

        public async Task<QueryResult<ReportMapEntity>> Query(Query query)
        {
            var result = await _userGroupRepository.QueryUserGroup(query);

            return new QueryResult<ReportMapEntity>
            {
                TotalCount = result.TotalCount,
                Items = result.Items.Select(p => new ReportMapEntity
                {
                    Id = p.Id,
                    Name = p.Name
                }).ToList()
            };
        }

        public Guid Id(object reference)
        {
            if (reference is not UserGroup entity)
                throw new Exception($"Cannot get id of type {reference.GetType()}");
            return entity.Id;
        }

        public string Name(object reference)
        {
            if (reference is not UserGroup entity)
                throw new Exception($"Cannot get name of type {reference.GetType()}");
            return entity.Name;
        }
    }
}
