namespace Webfuel
{
    public class Query: IQueryFilterBuilder
    {
        public List<string> Projection { get; set; } = new List<string>();

        public List<QueryFilter> Filters { get; set; } = new List<QueryFilter>();

        public List<QuerySort> Sort { get; set; } = new List<QuerySort>();

        public int Skip { get; set; }

        public int Take { get; set; }
    }
}
