using MediatR;

namespace Webfuel.Domain
{
    internal class CreateProjectHandler : IRequestHandler<CreateProject, Project>
    {
        private readonly ICreateProjectService _createProjectService;

        public CreateProjectHandler(ICreateProjectService createProjectService)
        {
            _createProjectService = createProjectService;
        }

        public async Task<Project> Handle(CreateProject request, CancellationToken cancellationToken)
        {
            return await _createProjectService.CreateProject(request);
        }
    }
}