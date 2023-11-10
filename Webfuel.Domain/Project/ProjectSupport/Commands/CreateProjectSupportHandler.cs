using MediatR;

namespace Webfuel.Domain
{
    internal class CreateProjectSupportHandler : IRequestHandler<CreateProjectSupport, ProjectSupport>
    {
        private readonly IProjectSupportRepository _projectSupportRepository;

        public CreateProjectSupportHandler(IProjectSupportRepository projectSupportRepository)
        {
            _projectSupportRepository = projectSupportRepository;
        }

        public async Task<ProjectSupport> Handle(CreateProjectSupport request, CancellationToken cancellationToken)
        {
            var projectSupport = new ProjectSupport();

            projectSupport.ProjectId = request.ProjectId;
            projectSupport.Date = request.Date ?? DateOnly.FromDateTime(DateTime.Now);
            projectSupport.TeamIds = request.TeamIds;
            projectSupport.AdviserIds = request.AdviserIds;
            projectSupport.SupportProvidedIds = request.SupportProvidedIds;
            projectSupport.Description = request.Description;

            return await _projectSupportRepository.InsertProjectSupport(projectSupport);
        }
    }
}