namespace Webfuel
{
    public class Query
    {
        public List<QuerySort> Sort { get; set; } = new List<QuerySort>();

        public int Skip { get; set; }

        public int Take { get; set; }

        public virtual RepositoryQuery ToRepositoryQuery()
        {
            return new RepositoryQuery { Skip = Skip, Take = Take, Sort = Sort };
        }

    }
}
