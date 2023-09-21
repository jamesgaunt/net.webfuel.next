using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain.Common
{
    public class LoginUser : IRequest<IdentityToken>
    {
        public required string Email { get; set; }

        public required string Password { get; set; }
    }

    internal class LoginUserHandler : IRequestHandler<LoginUser, IdentityToken>
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

        public async Task<IdentityToken> Handle(LoginUser request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email);
            if (user == null)
            {
                if (request.Email != "james.gaunt@webfuel.com")
                    throw new InvalidOperationException("Invalid username or password");
                user = await BootstrapDeveloperUser();
            }

            var validated = AuthenticationUtility.ValidatePassword(request.Password, user.PasswordHash, user.PasswordSalt);
            if (!validated)
                throw new InvalidOperationException("Invalid username or password");

            var token = new IdentityToken
            {
                User = new IdentityUser { Id = user.Id, Email = user.Email },
                Claims = new IdentityClaims(),
                Validity = new IdentityValidity(),
                Signature = string.Empty
            };

            _identityTokenService.ActivateToken(token);

            return token;
        }

        async Task<User> BootstrapDeveloperUser()
        {
            var userGroup = await _userGroupRepository.GetUserGroupByNameAsync("Default");
            if (userGroup == null)
                userGroup = await _userGroupRepository.InsertUserGroupAsync(new UserGroup { Name = "Default" });
            return await _mediator.Send(new CreateUser { Email = "james.gaunt@webfuel.com", UserGroupId = userGroup.Id });
        }
    }
}
