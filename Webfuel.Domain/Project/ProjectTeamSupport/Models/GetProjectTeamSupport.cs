using MediatR;

namespace Webfuel.Domain
{
    public class GetProjectTeamSupport : IRequest<ProjectTeamSupport?>
    {
        public Guid Id { get; set; }
    }
}
