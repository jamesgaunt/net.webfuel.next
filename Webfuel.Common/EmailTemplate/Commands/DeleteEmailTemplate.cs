using MediatR;

namespace Webfuel.Common
{
    public class DeleteEmailTemplate: IRequest
    {
        public required Guid Id { get; set; }
    }
    internal class DeleteEmailTemplateHandler : IRequestHandler<DeleteEmailTemplate>
    {
        private readonly IEmailTemplateRepository _emailTemplateRepository;
        
        public DeleteEmailTemplateHandler(IEmailTemplateRepository emailTemplateRepository)
        {
            _emailTemplateRepository = emailTemplateRepository;
        }
        
        public async Task Handle(DeleteEmailTemplate request, CancellationToken cancellationToken)
        {
            await _emailTemplateRepository.DeleteEmailTemplate(request.Id);
        }
    }
}

