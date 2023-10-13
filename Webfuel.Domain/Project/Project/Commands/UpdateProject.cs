using MediatR;

namespace Webfuel.Domain
{
    internal class UpdateProjectHandler : IRequestHandler<UpdateProject, Project>
    {
        private readonly IProjectUpdateService _projectUpdateService;

        public UpdateProjectHandler(IProjectUpdateService projectUpdateService)
        {
            _projectUpdateService = projectUpdateService;
        }

        public async Task<Project> Handle(UpdateProject request, CancellationToken cancellationToken)
        {
            return await _projectUpdateService.UpdateProject(request);
        }
    }
}
