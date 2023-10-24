using MediatR;

namespace Webfuel.Domain
{
    public class GetProjectSubmission : IRequest<ProjectSubmission?>
    {
        public Guid Id { get; set; }
    }
}
