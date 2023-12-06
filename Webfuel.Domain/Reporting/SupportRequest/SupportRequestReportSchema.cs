using Microsoft.Extensions.Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Domain.StaticData;
using Webfuel.Reporting;

namespace Webfuel.Domain
{
    public static class SupportRequestReportSchema
    {
        public static ReportSchema Schema
        {
            get
            {
                if (_schema == null)
                {
                    var builder = new ReportSchemaBuilder<SupportRequest>(ReportProviderEnum.SupportRequest);

                    builder.AddProperty(Guid.Parse("82b05021-9512-4217-9e71-bb0bc9bc8384"), p => p.Number);
                    builder.AddProperty(Guid.Parse("c3b0b5a0-5b1a-4b7e-9b9a-0b6b8b8b6b8b"), p => p.PrefixedNumber);
                    builder.AddReference<ISupportRequestStatusReferenceProvider>(Guid.Parse("c3b0b5a0-5b1a-4b7e-9b9a-0b6b8b8b6b8b"), p => p.StatusId);

                    builder.AddProperty(Guid.Parse("cbeb9e2d-59a2-4896-a3c5-01c5c2aa42c7"), p => p.Title);
                    builder.AddProperty(Guid.Parse("edde730a-8424-4415-b23c-29c4ae3e36b8"), p => p.DateOfRequest);

                    _schema = builder.Schema;
                }

                return _schema;
            }
        }

        static ReportSchema? _schema = null;
    }
}
