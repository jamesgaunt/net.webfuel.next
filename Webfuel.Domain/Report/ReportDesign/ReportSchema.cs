using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    [ApiType]
    public interface IReportSchema
    {
        IEnumerable<IReportField> Fields { get; }
    }

    public class ReportSchema<TContext>: IReportSchema where TContext : class
    {
        public required IReadOnlyList<ReportField<TContext>> Fields { get; init; }

        IEnumerable<IReportField> IReportSchema.Fields => Fields;
    }
}
