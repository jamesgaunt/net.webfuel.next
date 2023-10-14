using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{
    public interface IReportingContext<TContext> where TContext : class
    {
        Guid Id { get; }

        string Name { get; }

        string Description { get; }

        IEnumerable<ReportingField<TContext>> Fields { get; }
    }
}
