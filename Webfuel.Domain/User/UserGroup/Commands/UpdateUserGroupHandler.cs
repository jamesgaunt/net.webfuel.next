using MediatR;

namespace Webfuel.Domain
{
    internal class UpdateUserGroupHandler : IRequestHandler<UpdateUserGroup, UserGroup>
    {
        private readonly IUserGroupRepository _userGroupRepository;

        public UpdateUserGroupHandler(IUserGroupRepository userGroupRepository)
        {
            _userGroupRepository = userGroupRepository;
        }

        public async Task<UserGroup> Handle(UpdateUserGroup request, CancellationToken cancellationToken)
        {
            var original = await _userGroupRepository.RequireUserGroup(request.Id);

            var updated = original.Copy();
            updated.Name = request.Name;

            return await _userGroupRepository.UpdateUserGroup(original: original, updated: updated); 
        }
    }
}
