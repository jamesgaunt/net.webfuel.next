namespace Webfuel
{
    public class Query: IQueryFilterBuilder
    {
        public int Skip { get; set; }

        public int Take { get; set; }

        [ApiOptional]
        public List<string> Projection { get; set; } = new List<string>();

        [ApiOptional]
        public List<QueryFilter> Filters { get; set; } = new List<QueryFilter>();

        [ApiOptional]
        public List<QuerySort> Sort { get; set; } = new List<QuerySort>();

        [ApiOptional]
        public string Search { get; set; } = String.Empty;
    }
}
