using MediatR;

namespace Webfuel.Domain
{
    internal class UpdateProjectHandler : IRequestHandler<UpdateProject, Project>
    {
        private readonly IUpdateProjectService _projectUpdateService;

        public UpdateProjectHandler(IUpdateProjectService projectUpdateService)
        {
            _projectUpdateService = projectUpdateService;
        }

        public async Task<Project> Handle(UpdateProject request, CancellationToken cancellationToken)
        {
            return await _projectUpdateService.UpdateProject(request);
        }
    }
}
