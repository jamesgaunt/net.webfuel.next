using System;
using System.Collections.Generic;
using System.Text;

namespace Webfuel
{
    public class RepositoryQueryResult<TItem>
    {
        public RepositoryQueryResult()
        {
            Items = new List<TItem>();
            TotalCount = 0;
        }

        public RepositoryQueryResult(List<TItem> items, int? totalCount = null)
        {
            Items = items;
            TotalCount = totalCount ?? items.Count;
        }

        public List<TItem> Items { get; set; }

        public int TotalCount { get; set; }
    }
}
