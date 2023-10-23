using FluentValidation;
using MediatR;

namespace Webfuel.Domain
{

    internal class GetProjectSupportHandler : IRequestHandler<GetProjectSupport, ProjectSupport?>
    {
        private readonly IProjectSupportRepository _projectSupportRepository;

        public GetProjectSupportHandler(IProjectSupportRepository projectSupportRepository)
        {
            _projectSupportRepository = projectSupportRepository;
        }

        public async Task<ProjectSupport?> Handle(GetProjectSupport request, CancellationToken cancellationToken)
        {
            return await _projectSupportRepository.GetProjectSupport(request.Id);
        }
    }
}
