using MediatR;

namespace Webfuel.Domain
{
    internal class GetUserGroupHandler : IRequestHandler<GetUserGroup, UserGroup?>
    {
        private readonly IUserGroupRepository _userGroupRepository;

        public GetUserGroupHandler(IUserGroupRepository userGroupRepository)
        {
            _userGroupRepository = userGroupRepository;
        }

        public async Task<UserGroup?> Handle(GetUserGroup request, CancellationToken cancellationToken)
        {
            return await _userGroupRepository.GetUserGroup(request.Id);
        }
    }
}
