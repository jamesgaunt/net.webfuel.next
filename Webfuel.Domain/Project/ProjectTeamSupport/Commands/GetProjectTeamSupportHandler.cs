using FluentValidation;
using MediatR;

namespace Webfuel.Domain
{

    internal class GetProjectTeamSupportHandler : IRequestHandler<GetProjectTeamSupport, ProjectTeamSupport?>
    {
        private readonly IProjectTeamSupportRepository _projectTeamSupportRepository;

        public GetProjectTeamSupportHandler(IProjectTeamSupportRepository projectTeamSupportRepository)
        {
            _projectTeamSupportRepository = projectTeamSupportRepository;
        }

        public async Task<ProjectTeamSupport?> Handle(GetProjectTeamSupport request, CancellationToken cancellationToken)
        {
            return await _projectTeamSupportRepository.GetProjectTeamSupport(request.Id);
        }
    }
}
