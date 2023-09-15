using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{
    [TypefuelInterface]
    public class Query<TFilter> where TFilter : class
    {
        public List<QuerySort> Sort { get; set; } = new List<QuerySort>();

        public int Skip { get; set; }

        public int Take { get; set; }

        public TFilter? Filter { get; set; }
    }

    [TypefuelInterface]
    public class SimpleQuery: Query<Object>
    { }

    [TypefuelInterface]
    public class SearchFilter
    {
        public string Search { get; set; } = String.Empty;
    }

    [TypefuelInterface]
    public class SearchQuery: Query<SearchFilter>
    { }

}
