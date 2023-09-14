using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Core
{
    [TypefuelInterface]
    public class Query
    {
        public List<QuerySort> Sort { get; set; } = new List<QuerySort>();

        public int Skip { get; set; }

        public int Take { get; set; }
    }
}
