using MediatR;

namespace Webfuel.Domain
{
    internal class UpdateProjectSupportHandler : IRequestHandler<UpdateProjectSupport, ProjectSupport>
    {
        private readonly IProjectSupportRepository _projectSupportRepository;

        public UpdateProjectSupportHandler(IProjectSupportRepository projectSupportRepository)
        {
            _projectSupportRepository = projectSupportRepository;
        }

        public async Task<ProjectSupport> Handle(UpdateProjectSupport request, CancellationToken cancellationToken)
        {
            var projectSupport = await _projectSupportRepository.RequireProjectSupport(request.Id);

            var updated = projectSupport.Copy();
            updated.Date = request.Date;
            updated.AdviserIds = request.AdviserIds;
            updated.SupportProvidedIds = request.SupportProvidedIds;
            updated.Description = request.Description;

            return await _projectSupportRepository.UpdateProjectSupport(updated: updated, original: projectSupport);
        }
    }
}