using MediatR;

namespace Webfuel.Domain
{
    internal class UpdateProjectRequestHandler : IRequestHandler<UpdateProjectRequest, Project>
    {
        private readonly IUpdateProjectService _updateProjectService;

        public UpdateProjectRequestHandler(IUpdateProjectService updateProjectService)
        {
            _updateProjectService = updateProjectService;
        }

        public async Task<Project> Handle(UpdateProjectRequest request, CancellationToken cancellationToken)
        {
            return await _updateProjectService.UpdateProjectRequest(request);
        }
    }
}