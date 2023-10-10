using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class DeleteGender: IRequest
    {
        public required Guid Id { get; set; }
    }
    internal class DeleteGenderHandler : IRequestHandler<DeleteGender>
    {
        private readonly IGenderRepository _genderRepository;
        
        
        public DeleteGenderHandler(IGenderRepository genderRepository)
        {
            _genderRepository = genderRepository;
        }
        
        public async Task Handle(DeleteGender request, CancellationToken cancellationToken)
        {
            await _genderRepository.DeleteGender(request.Id);
        }
    }
}

