using Microsoft.AspNetCore.Routing;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    public class ReportSchema
    {
        public List<ReportField> Fields { get; } = new List<ReportField>();
    }

    public class ReportSchemaBuilder<TContext> where TContext: class
    {
        private readonly ReportSchema Schema = new ReportSchema();
    }
}
