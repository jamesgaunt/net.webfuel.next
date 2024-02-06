using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    public interface IUserSortService
    {
        Task<List<Guid>> SortUserIds(List<Guid> ids);
    }

    [Service(typeof(IUserSortService))]
    internal class UserSortService: IUserSortService
    {
        private readonly IUserRepository _userRepository;

        public UserSortService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<Guid>> SortUserIds(List<Guid> ids)
        {
            var users = new List<User>();
            foreach(var id in ids)
            {
                if (users.Any(p => p.Id == id))
                    continue;

                var user = await _userRepository.GetUser(id);
                if (user != null)
                    users.Add(user);
            }
            return users.OrderBy(p => p.LastName).Select(p => p.Id).ToList();
        }
    }
}
