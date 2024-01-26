using MediatR;

namespace Webfuel.Common
{
    public class CreateEmailTemplate: IRequest<EmailTemplate>
    {
        public required string Name { get; set; }
    }
    internal class CreateEmailTemplateHandler : IRequestHandler<CreateEmailTemplate, EmailTemplate>
    {
        private readonly IEmailTemplateRepository _emailTemplateRepository;
        
        public CreateEmailTemplateHandler(IEmailTemplateRepository emailTemplateRepository)
        {
            _emailTemplateRepository = emailTemplateRepository;
        }
        public async Task<EmailTemplate> Handle(CreateEmailTemplate request, CancellationToken cancellationToken)
        {
            var updated = await _emailTemplateRepository.InsertEmailTemplate(new EmailTemplate {
                    Name = request.Name,
            });
            
            return updated;
        }
    }
}

