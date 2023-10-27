using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Webfuel.Domain
{
    public class CreateResearcher : IRequest<Researcher>
    {
        public required string Email { get; set; }
    }

    internal class CreateResearcherHandler : IRequestHandler<CreateResearcher, Researcher>
    {
        private readonly IResearcherRepository _researcherRepository;

        public CreateResearcherHandler(IResearcherRepository researcherRepository)
        {
            _researcherRepository = researcherRepository;
        }

        public async Task<Researcher> Handle(CreateResearcher request, CancellationToken cancellationToken)
        {
            return await _researcherRepository.InsertResearcher(new Researcher
            {
                Email = request.Email,
                CreatedAt = DateTimeOffset.UtcNow,
            });
        }
    }
}
