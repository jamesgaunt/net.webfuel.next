using FluentValidation;
using MediatR;

namespace Webfuel.Domain
{
    public class GetProject : IRequest<Project?>
    {
        public Guid Id { get; set; }
    }

    internal class GetProjectHandler : IRequestHandler<GetProject, Project?>
    {
        private readonly IProjectRepository _projectRepository;

        public GetProjectHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Project?> Handle(GetProject request, CancellationToken cancellationToken)
        {
            return await _projectRepository.GetProject(request.Id);
        }
    }
}
