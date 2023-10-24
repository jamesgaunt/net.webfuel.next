using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class DeleteUserDiscipline: IRequest
    {
        public required Guid Id { get; set; }
    }
    internal class DeleteUserDisciplineHandler : IRequestHandler<DeleteUserDiscipline>
    {
        private readonly IUserDisciplineRepository _userDisciplineRepository;
        
        
        public DeleteUserDisciplineHandler(IUserDisciplineRepository userDisciplineRepository)
        {
            _userDisciplineRepository = userDisciplineRepository;
        }
        
        public async Task Handle(DeleteUserDiscipline request, CancellationToken cancellationToken)
        {
            await _userDisciplineRepository.DeleteUserDiscipline(request.Id);
        }
    }
}

