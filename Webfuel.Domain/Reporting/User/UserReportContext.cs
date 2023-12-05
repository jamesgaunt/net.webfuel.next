using DocumentFormat.OpenXml.Office.CustomUI;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Reporting;

namespace Webfuel.Domain
{
    public class UserReportContext
    {
        private readonly IServiceProvider _serviceProvider;

        public UserReportContext(User item, IServiceProvider serviceProvider)
        {
            Item = item;
            _serviceProvider = serviceProvider;
        }

        public User Item { get; private set; }

        public async Task<string> UserGroup()
        {
            var userGroup = await _serviceProvider.GetRequiredService<IUserGroupRepository>().GetUserGroup(Item.UserGroupId);
            return userGroup?.Name ?? string.Empty;
        }
    }
}
