using MediatR;

namespace Webfuel.Domain
{
    internal class CreateProjectHandler : IRequestHandler<CreateProject, Project>
    {
        private readonly IProjectCreateService _projectCreateService;

        public CreateProjectHandler(IProjectCreateService projectCreateService)
        {
            _projectCreateService = projectCreateService;
        }

        public async Task<Project> Handle(CreateProject request, CancellationToken cancellationToken)
        {
            return await _projectCreateService.CreateProject(request);
        }
    }
}