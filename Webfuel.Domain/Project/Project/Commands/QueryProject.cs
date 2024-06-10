using MediatR;

namespace Webfuel.Domain
{
    public class QueryProject : Query, IRequest<QueryResult<Project>>
    {
        public Query ApplyCustomFilters()
        {
            var number = FilterUtility.ExtractInt32(Number);

            this.Contains(nameof(Project.Title), Title);
            this.Equal(nameof(Project.Number), number, number != null);
            this.GreaterThanOrEqual(nameof(Project.DateOfRequest), FromDate, FromDate != null);
            this.LessThanOrEqual(nameof(Project.DateOfRequest), ToDate, ToDate != null);
            this.Equal(nameof(Project.StatusId), StatusId, StatusId != null);
            this.Equal(nameof(Project.LeadAdviserUserId), LeadAdviserUserId, LeadAdviserUserId != null);
            this.Equal(nameof(Project.ProposedFundingStreamId), ProposedFundingStreamId, ProposedFundingStreamId != null);
            this.Contains(nameof(Project.TeamContactFullName), TeamContactName);

            if (RequestedSupportTeamId.HasValue)
                this.SQL($"EXISTS (SELECT Id FROM [ProjectSupport] AS ps WHERE ps.[ProjectId] = e.Id AND ps.[SupportRequestedTeamId] = '{RequestedSupportTeamId.Value}' AND ps.[SupportRequestedCompletedAt] IS NULL)");

            if (SupportAdviserUserId.HasValue)
                this.SQL($"EXISTS (SELECT Id FROM [ProjectAdviser] AS pa WHERE pa.[ProjectId] = e.Id AND pa.[UserId] = '{SupportAdviserUserId.Value}')");

            return this;
        }

        public string Number { get; set; } = String.Empty;

        public string Title { get; set; } = String.Empty;

        public DateOnly? FromDate { get; set; }

        public DateOnly? ToDate { get; set; }

        public Guid? StatusId { get; set; }

        public Guid? LeadAdviserUserId { get; set; }

        public Guid? SupportAdviserUserId { get; set; }

        public Guid? ProposedFundingStreamId { get; set; }

        public string TeamContactName { get; set; } = String.Empty;

        public Guid? RequestedSupportTeamId { get; set; }
    }

    internal class QueryProjectHandler : IRequestHandler<QueryProject, QueryResult<Project>>
    {
        private readonly IProjectRepository _projectRepository;

        public QueryProjectHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<QueryResult<Project>> Handle(QueryProject request, CancellationToken cancellationToken)
        {
            return await _projectRepository.QueryProject(request.ApplyCustomFilters());
        }
    }
}
