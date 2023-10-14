using MediatR;

namespace Webfuel.Domain
{
    internal class CreateUserGroupHandler : IRequestHandler<CreateUserGroup, UserGroup>
    {
        private readonly IUserGroupRepository _userGroupRepository;

        public CreateUserGroupHandler(IUserGroupRepository userGroupRepository)
        {
            _userGroupRepository = userGroupRepository;
        }

        public async Task<UserGroup> Handle(CreateUserGroup request, CancellationToken cancellationToken)
        {
            return await _userGroupRepository.InsertUserGroup(new UserGroup { Name = request.Name });
        }
    }
}
