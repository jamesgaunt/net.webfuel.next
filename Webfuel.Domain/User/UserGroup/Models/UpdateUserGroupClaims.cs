using FluentValidation;
using MediatR;

namespace Webfuel.Domain
{
    public class UpdateUserGroupClaims : IRequest<UserGroup>
    {
        public Guid Id { get; set; }

        public bool CanEditUsers { get; set; }

        public bool CanEditStaticData { get; set; }

        public bool CanEditResearchers { get; set; }
    }
}
