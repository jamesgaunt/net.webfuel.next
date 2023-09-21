using FluentValidation;
using MediatR;

namespace Webfuel.Domain.Common
{
    public class GetUserGroup : IRequest<UserGroup?>
    {
        public Guid Id { get; set; }
    }

    internal class GetUserGroupHandler : IRequestHandler<GetUserGroup, UserGroup?>
    {
        private readonly IUserGroupRepository _userGroupRepository;

        public GetUserGroupHandler(IUserGroupRepository userGroupRepository)
        {
            _userGroupRepository = userGroupRepository;
        }

        public async Task<UserGroup?> Handle(GetUserGroup request, CancellationToken cancellationToken)
        {
            return await _userGroupRepository.GetUserGroupAsync(request.Id);
        }
    }
}
