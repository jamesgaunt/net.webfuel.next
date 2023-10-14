using MediatR;

namespace Webfuel.Domain
{
    internal class CreateProjectHandler : IRequestHandler<CreateProject, Project>
    {
        private readonly ICreateProjectService _projectCreateService;

        public CreateProjectHandler(ICreateProjectService projectCreateService)
        {
            _projectCreateService = projectCreateService;
        }

        public async Task<Project> Handle(CreateProject request, CancellationToken cancellationToken)
        {
            return await _projectCreateService.CreateProject(request);
        }
    }
}