using System;
using System.Collections.Generic;
using System.Text;

namespace Webfuel
{
    [TypefuelInterface]
    public class QueryResult<T>
    {
        public QueryResult()
        {
            Items = new List<T>();
            TotalCount = 0;
        }

        public QueryResult(List<T> items, int? totalCount = null)
        {
            Items = items;
            TotalCount = totalCount ?? items.Count;
        }

        public List<T> Items { get; set; }

        public int TotalCount { get; set; }
    }
}
