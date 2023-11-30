using Microsoft.Extensions.Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    public class ProjectReportSchema: ReportSchema<ProjectReportContext>
    {
        public ProjectReportSchema()
        {
            InitialiseFields();            
        }

        void InitialiseFields()
        {
            Fields.Add(new ReportField<ProjectReportContext>("Prefixed Number")
            {
                FieldType = ReportFieldType.String,
                Accessor = p => p.Item.PrefixedNumber
            });

            Fields.Add(new ReportField<ProjectReportContext>("Title")
            {
                FieldType = ReportFieldType.String,
                Accessor = p => p.Item.Title
            });
        }
    }
}
