namespace Webfuel
{
    public class Query<TFilter> where TFilter : class
    {
        public List<QuerySort> Sort { get; set; } = new List<QuerySort>();

        public int Skip { get; set; }

        public int Take { get; set; }

        public TFilter? Filter { get; set; }

        public virtual RepositoryQuery ToRepositoryQuery()
        {
            return new RepositoryQuery { Skip = Skip, Take = Take, Sort = Sort };
        }
    }

    public class SimpleQuery : Query<Object>
    {
    }

    public class SearchFilter
    {
        public string Search { get; set; } = String.Empty;
    }

    public class SearchQuery : Query<SearchFilter>
    { }

}
