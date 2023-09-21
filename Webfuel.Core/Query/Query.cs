namespace Webfuel
{
    public class Query: IQueryFilterBuilder
    {
        [ApiOptional]
        public List<string> Projection { get; set; } = new List<string>();

        [ApiOptional]
        public List<QueryFilter> Filters { get; set; } = new List<QueryFilter>();

        [ApiOptional]
        public List<QuerySort> Sort { get; set; } = new List<QuerySort>();

        [ApiOptional]
        public int Skip { get; set; }

        [ApiOptional]
        public int Take { get; set; }

        [ApiOptional]
        public string Search { get; set; } = String.Empty;
    }
}
