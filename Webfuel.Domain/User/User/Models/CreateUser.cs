using FluentValidation;
using MediatR;

namespace Webfuel.Domain
{
    public class CreateUser : IRequest<User>
    {
        public required string Email { get; set; }

        public required Guid UserGroupId { get; set; }
    }
}
