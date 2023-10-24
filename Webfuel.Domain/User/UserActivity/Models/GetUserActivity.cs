using MediatR;

namespace Webfuel.Domain
{
    public class GetUserActivity : IRequest<UserActivity?>
    {
        public Guid Id { get; set; }
    }
}
