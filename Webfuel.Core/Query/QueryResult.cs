using System;
using System.Collections.Generic;
using System.Text;

namespace Webfuel
{
    [TypefuelInterface]
    public class QueryResult<TItem, TQuery> where TQuery: class
    {
        public QueryResult()
        {
            Items = new List<TItem>();
            TotalCount = 0;
        }

        public QueryResult(List<TItem> items, int? totalCount = null, TQuery? query = null)
        {
            Items = items;
            TotalCount = totalCount ?? items.Count;
            Query = query;
        }

        public List<TItem> Items { get; set; }

        public int TotalCount { get; set; }

        public TQuery? Query { get; set; }
    }
}
