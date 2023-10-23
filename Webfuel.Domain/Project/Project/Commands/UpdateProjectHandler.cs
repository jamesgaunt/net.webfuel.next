using MediatR;

namespace Webfuel.Domain
{
    internal class UpdateProjectHandler : IRequestHandler<UpdateProject, Project>
    {
        private readonly IUpdateProjectService _updateProjectService;

        public UpdateProjectHandler(IUpdateProjectService updateProjectService)
        {
            _updateProjectService = updateProjectService;
        }

        public async Task<Project> Handle(UpdateProject request, CancellationToken cancellationToken)
        {
            return await _updateProjectService.UpdateProject(request);
        }
    }
}
