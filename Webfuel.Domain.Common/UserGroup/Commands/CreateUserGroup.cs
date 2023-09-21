using FluentValidation;
using MediatR;

namespace Webfuel.Domain.Common
{
    public class CreateUserGroup : IRequest<UserGroup>
    {
        public required string Name { get; set; }
    }

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
