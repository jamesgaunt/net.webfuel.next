using FluentValidation;
using MediatR;

namespace Webfuel.Domain
{
    public class UpdateUser : IRequest<User>
    {
        public Guid Id { get; set; }

        public string Email { get; set; } = String.Empty;

        public Guid UserGroupId { get; set; }
    }
}
