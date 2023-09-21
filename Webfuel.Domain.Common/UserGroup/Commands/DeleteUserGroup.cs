using MediatR;

namespace Webfuel.Domain.Common
{
    public class DeleteUserGroup : IRequest
    {
        public Guid Id { get; set; }
    }

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
