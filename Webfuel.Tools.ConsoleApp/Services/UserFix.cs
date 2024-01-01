using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Domain;
using Webfuel.Domain.StaticData;
using Webfuel.Excel;

namespace Webfuel.Tools.ConsoleApp
{
    public interface IUserFix
    {
        Task FixUsers();
    }

    [Service(typeof(IUserFix))]
    internal class UserFix: IUserFix
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserGroupRepository _userGroupRepository;
        private readonly ISupportTeamUserRepository _supportTeamUserRepository;
        private readonly IStaticDataService _staticDataService;

        public UserFix(
            IUserRepository userRepository, 
            IStaticDataService staticDataService, 
            IUserGroupRepository userGroupRepository,
            ISupportTeamUserRepository supportTeamUserRepository)
        {
            _userRepository = userRepository;
            _staticDataService = staticDataService;
            _userGroupRepository = userGroupRepository;
            _supportTeamUserRepository = supportTeamUserRepository;
        }

        public async Task FixUsers()
        {
            var users = await _userRepository.SelectUser();

            foreach(var user in users)
            {
                user.FullName = user.FirstName + " " + user.LastName;
                await _userRepository.UpdateUser(user);
            }


        }
    }

}
