using FluentValidation;
using MediatR;

namespace Webfuel.Domain
{
    public class GetUser : IRequest<User?>
    {
        public Guid Id { get; set; }
    }
}
