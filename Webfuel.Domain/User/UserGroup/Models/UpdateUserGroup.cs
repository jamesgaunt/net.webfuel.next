using FluentValidation;
using MediatR;

namespace Webfuel.Domain
{
    public class UpdateUserGroup : IRequest<UserGroup>
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = String.Empty;
    }
}
