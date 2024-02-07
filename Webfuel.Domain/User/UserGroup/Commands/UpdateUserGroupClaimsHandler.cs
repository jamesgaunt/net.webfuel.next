using MediatR;

namespace Webfuel.Domain
{
    internal class UpdateUserGroupClaimsHandler : IRequestHandler<UpdateUserGroupClaims, UserGroup>
    {
        private readonly IUserGroupRepository _userGroupRepository;

        public UpdateUserGroupClaimsHandler(IUserGroupRepository userGroupRepository)
        {
            _userGroupRepository = userGroupRepository;
        }

        public async Task<UserGroup> Handle(UpdateUserGroupClaims request, CancellationToken cancellationToken)
        {
            var original = await _userGroupRepository.RequireUserGroup(request.Id);

            var updated = original.Copy();

            updated.Claims.CanEditUsers = request.CanEditUsers;
            updated.Claims.CanEditUserGroups = request.CanEditUserGroups;
            updated.Claims.CanEditStaticData = request.CanEditStaticData;
            updated.Claims.CanEditReports = request.CanEditReports;
            updated.Claims.CanUnlockProjects = request.CanUnlockProjects;
            updated.Claims.CanTriageSupportRequests = request.CanTriageSupportRequests;

            return await _userGroupRepository.UpdateUserGroup(original: original, updated: updated); 
        }
    }
}
