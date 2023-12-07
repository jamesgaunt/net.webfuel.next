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
    public interface IUserTestData
    {
        Task GenerateTestData();
    }

    [Service(typeof(IUserTestData))]
    internal class UserTestData: IUserTestData
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserGroupRepository _userGroupRepository;
        private readonly ISupportTeamUserRepository _supportTeamUserRepository;
        private readonly IStaticDataService _staticDataService;

        public UserTestData(
            IUserRepository userRepository, 
            IStaticDataService staticDataService, 
            IUserGroupRepository userGroupRepository,
            ISupportTeamUserRepository supportTeamUserRepository)
        {
            _userRepository = userRepository;
            _staticDataService = staticDataService;
            _userGroupRepository = userGroupRepository;
            _supportTeamUserRepository = supportTeamUserRepository;

            UserGroups = _userGroupRepository.SelectUserGroup().GetAwaiter().GetResult();
            SupportTeams = _supportTeamUserRepository.SelectSupportTeamUser().GetAwaiter().GetResult();
            StaticData = _staticDataService.GetStaticData().GetAwaiter().GetResult();
        }

        List<UserGroup> UserGroups;

        List<SupportTeamUser> SupportTeams;

        IStaticDataModel StaticData;

        public async Task GenerateTestData()
        {
            var users = await _userRepository.SelectUser();

            if (users.Count > 10)
                throw new InvalidOperationException("Too many users exist already");

            Console.WriteLine($"Found {users.Count} users");


            for (var i = 0; i < 10000; i++) {

                var user = new User
                {
                    UserGroupId = UserGroups[_random.Next(UserGroups.Count)].Id,
                    Title = "Mr",
                    FirstName = RandomString(10),
                    LastName = RandomString(10),
                    Email = $"{RandomString(10)}@{RandomString(10)}.com",
                };
                await _userRepository.InsertUser(user);

                if (i % 100 == 0)
                    Console.WriteLine($"Inserted {i} users");
            }

        }


        static Random _random = new Random();

        static string RandomString(int length)
        {
            const string chars = "abcdefghiklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }

}
