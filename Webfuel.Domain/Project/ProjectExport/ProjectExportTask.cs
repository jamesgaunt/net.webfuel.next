using Webfuel.Common;
using Webfuel.Excel;

namespace Webfuel.Domain
{
    public class ProjectExportTask: ReportTask
    {
        public ProjectExportTask(ProjectExportRequest request)
        {
            Request = request;
            Query = Request.ToQuery();
        } 

        public override Type ReportGeneratorType => typeof(IProjectExportService);
       
        public ProjectExportRequest Request { get; }
        
        public QueryProject Query { get; }  
       

        public ExcelData Data { get; } = new ExcelData();
    }
}
