using MediatR;

namespace Webfuel.Domain
{
    internal class UpdateProjectResearcherHandler : IRequestHandler<UpdateProjectResearcher, Project>
    {
        private readonly IUpdateProjectService _updateProjectService;

        public UpdateProjectResearcherHandler(IUpdateProjectService updateProjectService)
        {
            _updateProjectService = updateProjectService;
        }

        public async Task<Project> Handle(UpdateProjectResearcher request, CancellationToken cancellationToken)
        {
            return await _updateProjectService.UpdateProjectResearcher(request);
        }
    }
}