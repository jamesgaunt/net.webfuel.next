using MediatR;

namespace Webfuel.Domain
{
    internal class UpdateProjectStatusHandler : IRequestHandler<UpdateProjectStatus, Project>
    {
        private readonly IUpdateProjectService _updateProjectService;

        public UpdateProjectStatusHandler(IUpdateProjectService updateProjectService)
        {
            _updateProjectService = updateProjectService;
        }

        public async Task<Project> Handle(UpdateProjectStatus request, CancellationToken cancellationToken)
        {
            return await _updateProjectService.UpdateProjectStatus(request);
        }
    }
}
