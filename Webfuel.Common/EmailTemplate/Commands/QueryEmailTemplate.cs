using MediatR;

namespace Webfuel.Common
{
    public class QueryEmailTemplate: Query, IRequest<QueryResult<EmailTemplate>>
    {
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(EmailTemplate.Name), Search);
            return this;
        }
    }
    internal class QueryEmailTemplateHandler : IRequestHandler<QueryEmailTemplate, QueryResult<EmailTemplate>>
    {
        private readonly IEmailTemplateRepository _emailTemplateRepository;
        
        
        public QueryEmailTemplateHandler(IEmailTemplateRepository emailTemplateRepository)
        {
            _emailTemplateRepository = emailTemplateRepository;
        }
        
        public async Task<QueryResult<EmailTemplate>> Handle(QueryEmailTemplate request, CancellationToken cancellationToken)
        {
            return await _emailTemplateRepository.QueryEmailTemplate(request.ApplyCustomFilters());
        }
    }
}

