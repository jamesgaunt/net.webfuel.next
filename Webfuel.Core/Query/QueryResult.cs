namespace Webfuel
{
    public class QueryResult<TItem>
    {
        public QueryResult()
        {
            Items = new List<TItem>();
            TotalCount = 0;
        }

        public QueryResult(List<TItem> items, int totalCount)
        {
            Items = items;
            TotalCount = totalCount;
        }

        public List<TItem> Items { get; set; }

        public int TotalCount { get; set; }
    }
}
