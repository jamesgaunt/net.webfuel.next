using MediatR;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace Webfuel.Domain
{
    internal class LoginUserHandler : IRequestHandler<LoginUser, StringResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserGroupRepository _userGroupRepository;
        private readonly IUserLoginRepository _userLoginRepository;
        private readonly IIdentityTokenService _identityTokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginUserHandler(
            IUserRepository userRepository,
            IUserGroupRepository userGroupRepository,
            IUserLoginRepository userLoginRepository,
            IIdentityTokenService identityTokenService,
            IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _identityTokenService = identityTokenService;
            _userGroupRepository = userGroupRepository;
            _userLoginRepository = userLoginRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<StringResult> Handle(LoginUser request, CancellationToken cancellationToken)
        {
            var userLogin = new UserLogin
            {
                Email = request.Email,
                CreatedAt = DateTimeOffset.UtcNow,
                IPAddress = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? String.Empty
            };

            try
            {
                await Sanitize(request);

                var user = await _userRepository.GetUserByEmail(request.Email);
                if (user == null)
                {
                    if (request.Email != "james.gaunt@webfuel.com")
                        throw new DomainException("Invalid username or password");
                    user = await BootstrapDeveloperUser("james.gaunt@webfuel.com");
                }

                userLogin.UserId = user.Id;


                if (user == null || user.Disabled)
                    throw new DomainException("Invalid username or password");

                var validated = AuthenticationUtility.ValidatePassword(request.Password, user.PasswordHash, user.PasswordSalt);
                if (!validated)
                    throw new DomainException("Invalid username or password");

                userLogin.Successful = true;

                return new StringResult(await _identityTokenService.GenerateToken(new IdentityUser
                {
                    Id = user.Id,
                    Email = user.Email
                }));
            }
            catch (Exception e)
            {
                userLogin.Successful = false;
                userLogin.Reason = e.Message;
                throw;
            }
            finally
            {
                try
                {
                    await _userLoginRepository.InsertUserLogin(userLogin);
                }
                catch { /* GULP */ }
            }
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
                Developer = true,
                CreatedAt = DateTimeOffset.UtcNow,
            });
        }

        Task Sanitize(LoginUser request)
        {
            request.Email = request.Email.Trim();
            request.Password = request.Password.Trim();
            return Task.CompletedTask;
        }
    }
}
