using Microsoft.AspNetCore.Routing;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    public class ReportSchema
    {
        public required Guid ReportProviderId { get; init; }

        public IReadOnlyList<ReportField> Fields => _fields;

        List<ReportField> _fields = new List<ReportField>();

        internal void AddField(ReportField field)
        {
            if (_fields.Any(p => p.Id == field.Id))
                throw new InvalidOperationException($"Field {field.Name} has an id that is already in use.");
            _fields.Add(field);
        }
    }
}
