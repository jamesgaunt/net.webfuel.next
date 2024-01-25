namespace Webfuel.Domain
{
    public class ProjectExportRequest
    {
        public string Number { get; set; } = String.Empty;

        public string Title { get; set; } = String.Empty;

        public DateOnly? FromDate { get; set; }

        public DateOnly? ToDate { get; set; }

        public Guid? StatusId { get; set; }

        public Guid? ProposedFundingStreamId { get; set; }

        public Query ToQuery()
        {
            var query = new QueryProject
            {
                Number = Number,
                Title = Title,
                FromDate = FromDate,
                ToDate = ToDate,
                StatusId = StatusId,
                ProposedFundingStreamId = ProposedFundingStreamId,

                Skip = 0,
                Take = 1
            };
            query.ApplyCustomFilters();
            return query;
        }
    }
}
