using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    public enum ProjectDiagnosticSeverity
    {
        Error = 10,
        Warning = 20,
    }

    public class ProjectDiagnostic
    {
        public required ProjectDiagnosticSeverity Severity { get; set; }

        public required string Message { get; set; }

        public static ProjectDiagnostic Error(string message)
        {
            return new ProjectDiagnostic
            {
                Severity = ProjectDiagnosticSeverity.Error,
                Message = message
            };
        }

        public static ProjectDiagnostic Warning(string message)
        {
            return new ProjectDiagnostic
            {
                Severity = ProjectDiagnosticSeverity.Warning,
                Message = message
            };
        }
    }
}
