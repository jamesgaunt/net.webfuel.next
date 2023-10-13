using FluentValidation;
using MediatR;

namespace Webfuel.Domain
{
    public class GetResearcher : IRequest<Researcher?>
    {
        public Guid Id { get; set; }
    }

    internal class GetResearcherHandler : IRequestHandler<GetResearcher, Researcher?>
    {
        private readonly IResearcherRepository _researcherRepository;

        public GetResearcherHandler(IResearcherRepository researcherRepository)
        {
            _researcherRepository = researcherRepository;
        }

        public async Task<Researcher?> Handle(GetResearcher request, CancellationToken cancellationToken)
        {
            return await _researcherRepository.GetResearcher(request.Id);
        }
    }
}
