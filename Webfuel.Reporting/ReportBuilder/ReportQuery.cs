using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    public abstract class ReportQuery
    {
        public Query Query { get; set; } = new Query();

        public abstract Task<IEnumerable<object>> GetItems(int skip, int take, IServiceProvider services);

        public abstract Task<int> GetTotalCount(IServiceProvider services);
    }
}
