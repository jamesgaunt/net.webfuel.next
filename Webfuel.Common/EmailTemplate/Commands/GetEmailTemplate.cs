using MediatR;

namespace Webfuel.Common
{
    public class GetEmailTemplate : IRequest<EmailTemplate?>
    {
        public Guid Id { get; set; }
    }

    internal class GetEmailTemplateHandler : IRequestHandler<GetEmailTemplate, EmailTemplate?>
    {
        private readonly IEmailTemplateRepository _reportRepository;

        public GetEmailTemplateHandler(IEmailTemplateRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task<EmailTemplate?> Handle(GetEmailTemplate request, CancellationToken cancellationToken)
        {
            return await _reportRepository.GetEmailTemplate(request.Id);
        }
    }
}
