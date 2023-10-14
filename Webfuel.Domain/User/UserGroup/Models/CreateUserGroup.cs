using FluentValidation;
using MediatR;

namespace Webfuel.Domain
{
    public class CreateUserGroup : IRequest<UserGroup>
    {
        public required string Name { get; set; }
    }
}
