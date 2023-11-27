namespace Webfuel.Domain
{
    public class ProjectExportRequest
    {
        public string Number { get; set; } = String.Empty;

        public string Title { get; set; } = String.Empty;

        public DateOnly? FromDate { get; set; }

        public DateOnly? ToDate { get; set; }

        public Guid? StatusId { get; set; }

        public Guid? FundingStreamId { get; set; }

        public QueryProject ToQuery()
        {
            var query = new QueryProject
            {
                Number = Number,
                Title = Title,
                FromDate = FromDate,
                ToDate = ToDate,
                StatusId = StatusId,
                FundingStreamId = FundingStreamId,

                Skip = 0,
                Take = 10
            };
            query.ApplyCustomFilters();
            return query;
        }
    }
}
