using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    public enum ProjectDiagnosticType
    {
        Enrichment = 10,
        Deferred = 20
    }

    public enum ProjectDiagnosticSeverity
    {
        Error = 10,
        Warning = 20,
    }

    public class ProjectDiagnostic
    {
        public required ProjectDiagnosticType Type { get; set; }

        public required ProjectDiagnosticSeverity Severity { get; set; }

        public required string Message { get; set; }

        public static ProjectDiagnostic EnrichmentError(string message)
        {
            return new ProjectDiagnostic
            {
                Type = ProjectDiagnosticType.Enrichment,
                Severity = ProjectDiagnosticSeverity.Error,
                Message = message
            };
        }

        public static ProjectDiagnostic EnrichmentWarning(string message)
        {
            return new ProjectDiagnostic
            {
                Type = ProjectDiagnosticType.Enrichment,
                Severity = ProjectDiagnosticSeverity.Warning,
                Message = message
            };
        }

        public static ProjectDiagnostic DeferredError(string message)
        {
            return new ProjectDiagnostic
            {
                Type = ProjectDiagnosticType.Deferred,
                Severity = ProjectDiagnosticSeverity.Error,
                Message = message
            };
        }

        public static ProjectDiagnostic DeferredWarning(string message)
        {
            return new ProjectDiagnostic
            {
                Type = ProjectDiagnosticType.Deferred,
                Severity = ProjectDiagnosticSeverity.Warning,
                Message = message
            };
        }
    }
}
