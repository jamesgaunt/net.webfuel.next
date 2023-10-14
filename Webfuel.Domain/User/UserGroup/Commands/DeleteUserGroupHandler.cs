using MediatR;

namespace Webfuel.Domain
{
    internal class DeleteUserGroupHandler : IRequestHandler<DeleteUserGroup>
    {
        private readonly IUserGroupRepository _userGroupRepository;

        public DeleteUserGroupHandler(IUserGroupRepository userGroupRepository)
        {
            _userGroupRepository = userGroupRepository;
        }

        public async Task Handle(DeleteUserGroup request, CancellationToken cancellationToken)
        {
            await _userGroupRepository.DeleteUserGroup(request.Id);
        }
    }
}
