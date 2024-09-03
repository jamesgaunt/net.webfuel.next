using MediatR;

namespace Webfuel.Domain;

public class QueryReport : Query, IRequest<QueryResult<Report>>
{
    public string Name { get; set; } = String.Empty;

    public Guid? ReportGroupId { get; set; }

    // This needs to be applied by the handler as it required an identity accessor
    public string OwnReportsOnly { get; set; } = "NO";

    public Query ApplyCustomFilters()
    {
        this.Contains(nameof(Report.Name), Name);
        this.Equal(nameof(Report.ReportGroupId), ReportGroupId, ReportGroupId != null);
        return this;
    }
}
