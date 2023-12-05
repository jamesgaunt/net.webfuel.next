using Microsoft.Extensions.Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Reporting;

namespace Webfuel.Domain
{
    public static class ProjectReportSchema
    {
        static ProjectReportSchema()
        {
            var builder = new ReportSchemaBuilder<Project>();

            builder.AddField(Guid.Parse("82b05021-9512-4217-9e71-bb0bc9bc8384"), p => p.Number);
            builder.AddField(Guid.Parse("cbeb9e2d-59a2-4896-a3c5-01c5c2aa42c7"), p => p.Title);
            builder.AddField(Guid.Parse("edde730a-8424-4415-b23c-29c4ae3e36b8"), p => p.DateOfRequest);
            builder.AddField(Guid.Parse("f64ac394-a697-4f22-92cc-274571bc65ae"), p => p.ClosureDate);

            Schema = builder.Schema;
        }

        public static ReportSchema Schema { get; }
    }
}
