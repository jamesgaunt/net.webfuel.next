using FluentValidation;
using MediatR;

namespace Webfuel.Domain
{
    public class UpdateResearcher : IRequest<Researcher>
    {
        public Guid Id { get; set; }

        public string Email { get; set; } = String.Empty;
    }

    internal class UpdateResearcherHandler : IRequestHandler<UpdateResearcher, Researcher>
    {
        private readonly IResearcherRepository _researcherRepository;

        public UpdateResearcherHandler(IResearcherRepository researcherRepository)
        {
            _researcherRepository = researcherRepository;
        }

        public async Task<Researcher> Handle(UpdateResearcher request, CancellationToken cancellationToken)
        {
            var original = await _researcherRepository.RequireResearcher(request.Id);

            var updated = original.Copy();
            updated.Email = request.Email;

            return await _researcherRepository.UpdateResearcher(original: original, updated: updated); ;
        }
    }
}
