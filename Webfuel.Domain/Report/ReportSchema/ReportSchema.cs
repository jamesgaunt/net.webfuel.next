using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        public List<ReportField<TContext>> Fields { get; } = new List<ReportField<TContext>>();

        IEnumerable<IReportField> IReportSchema.Fields => Fields;
    }
}
