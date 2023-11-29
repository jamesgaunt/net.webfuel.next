using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Common;

namespace Webfuel.Domain
{
    public interface IProjectReportProvider: IReportProvider
    {
    }

    [Service(typeof(IProjectReportProvider))]
    internal class ProjectReportProvider : IProjectReportProvider
    {
        public Guid Id => ReportProviderEnum.Project;

        public Task<IReportSchema> GetReportSchema()
        {
            return Task.FromResult<IReportSchema>(Schema);
        }

        public Task<ReportProgress> InitialiseReport(ReportRequest request)
        {
            throw new NotImplementedException();
        }

        static ReportSchema<Project> Schema = new ReportSchema<Project>
        {
            Fields = new List<ReportField<Project>>
            {
                new ReportField<Project>
                {
                    FieldId = "PrefixedNumber",
                    Name = "Prefixed Number",
                    FieldType = ReportFieldType.String,
                    Accessor = p => p.PrefixedNumber
                },
                new ReportField<Project>
                {
                    FieldId = "Title",
                    Name = "Title",
                    FieldType = ReportFieldType.String,
                    Accessor = p => p.Title
                }
            }
        };
    }
}
