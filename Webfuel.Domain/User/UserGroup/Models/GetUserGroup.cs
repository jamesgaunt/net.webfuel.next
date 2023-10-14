using FluentValidation;
using MediatR;

namespace Webfuel.Domain
{
    public class GetUserGroup : IRequest<UserGroup?>
    {
        public Guid Id { get; set; }
    }
}
