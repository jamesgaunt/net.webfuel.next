using MediatR;

namespace Webfuel.Common
{
    public class UpdateEmailTemplate: IRequest<EmailTemplate>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
    }
    internal class UpdateEmailTemplateHandler : IRequestHandler<UpdateEmailTemplate, EmailTemplate>
    {
        private readonly IEmailTemplateRepository _emailTemplateRepository;

        public UpdateEmailTemplateHandler(IEmailTemplateRepository emailTemplateRepository)
        {
            _emailTemplateRepository = emailTemplateRepository;
        }
        
        public async Task<EmailTemplate> Handle(UpdateEmailTemplate request, CancellationToken cancellationToken)
        {
            var original = await _emailTemplateRepository.RequireEmailTemplate(request.Id);
            
            var updated = original.Copy();
            updated.Name = request.Name;
            
            updated = await _emailTemplateRepository.UpdateEmailTemplate(original: original, updated: updated);
            return updated;
        }
    }
}

