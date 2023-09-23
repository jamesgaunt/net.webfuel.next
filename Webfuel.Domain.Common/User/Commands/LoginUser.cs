using Azure.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain.Common
{
    public class LoginUser : IRequest<StringResult>
    {
        public required string Email { get; set; }

        public required string Password { get; set; }
    }

    internal class LoginUserHandler : IRequestHandler<LoginUser, StringResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserGroupRepository _userGroupRepository;
        private readonly IIdentityTokenService _identityTokenService;
        private readonly IMediator _mediator;

        public LoginUserHandler(IUserRepository userRepository, IUserGroupRepository userGroupRepository, IIdentityTokenService identityTokenService, IMediator mediator)
        {
            _userRepository = userRepository;
            _identityTokenService = identityTokenService;
            _mediator = mediator;
            _userGroupRepository = userGroupRepository;
        }

        public async Task<StringResult> Handle(LoginUser request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmail(request.Email);
            if (user == null)
            {
                if (request.Email != "james.gaunt@webfuel.com")
                    throw new InvalidOperationException("Invalid username or password");
                user = await BootstrapDeveloperUser("james.gaunt@webfuel.com");
            }

            var validated = AuthenticationUtility.ValidatePassword(request.Password, user.PasswordHash, user.PasswordSalt);
            if (!validated)
                throw new InvalidOperationException("Invalid username or password");

            return new StringResult(await _identityTokenService.GenerateToken(new IdentityUser { Id = user.Id, Email = user.Email }));
        }

        async Task<User> BootstrapDeveloperUser(string email)
        {
            var userGroup = await _userGroupRepository.GetUserGroupByName("Default");
            if (userGroup == null)
                userGroup = await _userGroupRepository.InsertUserGroup(new UserGroup { Name = "Default" });

            return await _userRepository.InsertUser(new User
            {
                Email = email,
                UserGroupId = userGroup.Id,
                Developer = true
            });
        }
    }
}
